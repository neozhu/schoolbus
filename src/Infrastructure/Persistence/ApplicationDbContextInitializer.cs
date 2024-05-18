using System.Reflection;
using CleanArchitecture.Blazor.Application.Constants.ClaimTypes;
using CleanArchitecture.Blazor.Application.Constants.Permission;
using CleanArchitecture.Blazor.Application.Constants.Role;
using CleanArchitecture.Blazor.Application.Constants.User;
using CleanArchitecture.Blazor.Domain.Enums;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence;
public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer() || _context.Database.IsNpgsql() || _context.Database.IsSqlite())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
            _context.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
    private static IEnumerable<string> GetAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = typeof(Permissions).GetNestedTypes();

        foreach (var module in modules)
        {
            var moduleName = string.Empty;
            var moduleDescription = string.Empty;

            var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fields)
            {
                var propertyValue = fi.GetValue(null);

                if (propertyValue is not null)
                    allPermissions.Add((string)propertyValue);
            }
        }

        return allPermissions;
    }

    private async Task TrySeedAsync()
    {
        var tenantId1 = Guid.NewGuid().ToString();
        var tenantId2 = Guid.NewGuid().ToString();
        // Default tenants
        if (!_context.Tenants.Any())
        {
            _context.Tenants.Add(new Tenant {Id= tenantId1, Name = "ORG-1", Description = "a company or customer's instance" });
            _context.Tenants.Add(new Tenant {Id= tenantId2, Name = "ORG-2", Description = "a company or customer's instance" });
            await _context.SaveChangesAsync();

        }

        // Default roles
        var role1 = new ApplicationRole(RoleName.SuperAdmin) { Description = "Super Admin Group" };
        var role2 = new ApplicationRole(RoleName.OrgAdmin) { Description = "ORG's Admin Group" };
        var role3 = new ApplicationRole(RoleName.Driver) { Description = "Driver's Admin Group" };
        var role4 = new ApplicationRole(RoleName.Parents) { Description = "Parents's Admin Group" };
        var role5 = new ApplicationRole(RoleName.Students) { Description = "Students's Admin Group" };
        var role6 = new ApplicationRole(RoleName.Basic) { Description = "Basic's Admin Group" };
        var permissions = GetAllPermissions();
        if (_roleManager.Roles.All(r => r.Name != role1.Name))
        {
            await _roleManager.CreateAsync(role1);
           
            foreach (var permission in permissions)
            {
                await _roleManager.AddClaimAsync(role1, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
        if (_roleManager.Roles.All(r => r.Name != role2.Name))
        {
            await _roleManager.CreateAsync(role2);
        }
        if (_roleManager.Roles.All(r => r.Name != role3.Name))
        {
            await _roleManager.CreateAsync(role3);
        }
        if (_roleManager.Roles.All(r => r.Name != role4.Name))
        {
            await _roleManager.CreateAsync(role4);
        }
        if (_roleManager.Roles.All(r => r.Name != role5.Name))
        {
            await _roleManager.CreateAsync(role5);
        }
        if (_roleManager.Roles.All(r => r.Name != role6.Name))
        {
            await _roleManager.CreateAsync(role6);
        }
        // Default users
        var administrator = new ApplicationUser { UserName = UserName.Administrator, Provider = "Local", IsActive = true, TenantId = _context.Tenants.First().Id, TenantName = _context.Tenants.First().Name, DisplayName = UserName.Administrator, Email = "new163@163.com", EmailConfirmed = true, ProfilePictureDataUrl = "https://s.gravatar.com/avatar/78be68221020124c23c665ac54e07074?s=80" };
        var demo = new ApplicationUser { UserName = UserName.Demo, IsActive = true, Provider = "Local", TenantId = _context.Tenants.First().Id, TenantName = _context.Tenants.First().Name, DisplayName = UserName.Demo, Email = "neozhu@126.com", EmailConfirmed = true, ProfilePictureDataUrl = "https://s.gravatar.com/avatar/ea753b0b0f357a41491408307ade445e?s=80" };


        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, UserName.DefaultPassword);
            await _userManager.AddToRolesAsync(administrator, new[] { role1.Name! });
        }
        if (_userManager.Users.All(u => u.UserName != demo.UserName))
        {
            await _userManager.CreateAsync(demo, UserName.DefaultPassword);
            await _userManager.AddToRolesAsync(demo, new[] { role2.Name! });
        }

        if (!_context.Schools.Any())
        {
            _context.Schools.Add(new School() { Name = "St Paul's School", TenantId = tenantId1, Phone = "", Contact = "", Address = "Lonsdale Road, London, SW13 9JT, United Kingdom" });
            _context.Schools.Add(new School() { Name = "Westminster School", TenantId = tenantId1, Phone = "", Contact = "", Address = "17 Dean's Yard, Westminster, London, SW1P 3PB, United Kingdom" });
            _context.Schools.Add(new School() { Name = "Guildford High School", TenantId = tenantId2, Phone = "", Contact = "", Address = "London Road, Guildford, Surrey, GU1 1SJ, United Kingdom" });
            _context.Schools.Add(new School() { Name = "King's College School - Wimbledon", TenantId = tenantId2, Phone = "", Contact = "", Address = "Southside, Wimbledon Common, London, SW19 4TT, United Kingdom" });
            await _context.SaveChangesAsync();
        }
        if (!_context.Buses.Any())
        {
            _context.Buses.Add(new Bus() {PlatNumber = "H3 YMV", TenantId = tenantId1,  Description= "Yellow bus with 30 seats", DeviceId= "HXXX XXX 01", Status="OK" });
            _context.Buses.Add(new Bus() { PlatNumber = "FAK 3IT", TenantId = tenantId1, Description = "Yellow bus with 40 seats", DeviceId = "HXXX XXX 02", Status = "OK" });
            _context.Buses.Add(new Bus() { PlatNumber = "C8 FAK", TenantId = tenantId2, Description = "Yellow bus with 30 seats", DeviceId = "HXXX XXX 03", Status = "OK" });
            _context.Buses.Add(new Bus() { PlatNumber = "YU51 IA", TenantId = tenantId2, Description = "Yellow bus with 40 seats", DeviceId = "HXXX XXX 04", Status = "OK" });
            await _context.SaveChangesAsync();
        }
        // Default data
        // Seed, if necessary
        if (!_context.KeyValues.Any())
        {
            _context.KeyValues.Add(new KeyValue { Name =  Picklist.Status, Value = "initialization", Text = "initialization", Description = "Status of workflow" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Status, Value = "processing", Text = "processing", Description = "Status of workflow" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Status, Value = "pending", Text = "pending", Description = "Status of workflow" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Status, Value = "finished", Text = "finished", Description = "Status of workflow" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Brand, Value = "Apple", Text = "Apple", Description = "Brand of production" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Brand, Value = "MI", Text = "MI", Description = "Brand of production" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Brand, Value = "Logitech", Text = "Logitech", Description = "Brand of production" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Brand, Value = "Linksys", Text = "Linksys", Description = "Brand of production" });

            _context.KeyValues.Add(new KeyValue { Name = Picklist.Unit, Value = "EA", Text = "EA", Description = "Unit of product" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Unit, Value = "KM", Text = "KM", Description = "Unit of product" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Unit, Value = "PC", Text = "PC", Description = "Unit of product" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Unit, Value = "KG", Text = "KG", Description = "Unit of product" });
            _context.KeyValues.Add(new KeyValue { Name = Picklist.Unit, Value = "ST", Text = "ST", Description = "Unit of product" });
            await _context.SaveChangesAsync();

        }
        if (!_context.Products.Any())
        {
            _context.Products.Add(new Product { Brand = "Apple", Name = "IPhone 13 Pro", Description = "Apple iPhone 13 Pro smartphone. Announced Sep 2021. Features 6.1″ display, Apple A15 Bionic chipset, 3095 mAh battery, 1024 GB storage.", Unit = "EA", Price = 999.98m });
            _context.Products.Add(new Product { Brand = "MI", Name = "MI 12 Pro", Description = "Xiaomi 12 Pro Android smartphone. Announced Dec 2021. Features 6.73″ display, Snapdragon 8 Gen 1 chipset, 4600 mAh battery, 256 GB storage.", Unit = "EA", Price = 199.00m });
            _context.Products.Add(new Product { Brand = "Logitech", Name = "MX KEYS Mini", Description = "Logitech MX Keys Mini Introducing MX Keys Mini – a smaller, smarter, and mightier keyboard made for creators. Type with confidence on a keyboard crafted for efficiency, stability, and...", Unit = "PA", Price = 99.90m });
            await _context.SaveChangesAsync();
        }
    }
}

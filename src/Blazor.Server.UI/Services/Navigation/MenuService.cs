using Blazor.Server.UI.Models.NavigationMenu;
using CleanArchitecture.Blazor.Application.Constants.Role;

namespace Blazor.Server.UI.Services.Navigation;

public class MenuService : IMenuService
{
    private readonly List<MenuSectionModel> _features = new()
    {
        new MenuSectionModel
        {
            Title = "Application",
            SectionItems = new List<MenuSectionItemModel>
            {
                new() { Title = "Home", Icon = Icons.Material.Filled.Home, Href = "/" },

                new()
                {
                    Title = "School",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.Castle,
                    Href = "/pages/schools",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Bus",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.DirectionsBus,
                    Href = "/pages/buses",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Pilot",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.FollowTheSigns,
                    Href = "/pages/pilots",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Itineraries",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.LinearScale,
                    Href = "/pages/itineraries",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Student",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.Groups,
                    Href = "/pages/students",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Parent",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.EscalatorWarning,
                    Href = "/pages/parents",
                    PageStatus = PageStatus.Completed
                },
                new()
                {
                    Title = "Scanner",
                    Roles = new[] { RoleName.SuperAdmin, RoleName.OrgAdmin },
                    Icon = Icons.Material.Filled.QrCodeScanner,
                    Href = "/pages/scanner",
                    PageStatus = PageStatus.Completed
                }
            }
        },
        new MenuSectionModel
        {
            Title = "MANAGEMENT",
            Roles = new[] { RoleName.SuperAdmin,RoleName.Admin },
            SectionItems = new List<MenuSectionItemModel>
            {
                new()
                {
                    IsParent = true,
                    Title = "Authorization",
                    Icon = Icons.Material.Filled.ManageAccounts,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new()
                        {
                            Title = "Multi-Tenant",
                            Href = "/system/tenants",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Users",
                            Href = "/identity/users",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Roles",
                            Href = "/identity/roles",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Profile",
                            Href = "/user/profile",
                            PageStatus = PageStatus.Completed
                        }
                    }
                },
                new()
                {
                    IsParent = true,
                    Title = "System",
                    Icon = Icons.Material.Filled.Devices,
                    MenuItems = new List<MenuSectionSubItemModel>
                    {
                        new()
                        {
                            Title = "Picklist",
                            Href = "/system/picklist",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Audit Trails",
                            Href = "/system/audittrails",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Log",
                            Href = "/system/logs",
                            PageStatus = PageStatus.Completed
                        },
                        new()
                        {
                            Title = "Jobs",
                            Href = "/jobs",
                            PageStatus = PageStatus.Completed,
                            Target = "_blank"
                        }
                    }
                }
            }
        }
    };

    public IEnumerable<MenuSectionModel> Features => _features;
}
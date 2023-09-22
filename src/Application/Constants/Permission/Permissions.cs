// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Constants.Permission;

public static class Permissions
{
    [DisplayName("Schools")]
    [Description("Schools Permissions")]
    public static class Schools
    {
        public const string View = "Permissions.Schools.View";
        public const string Create = "Permissions.Schools.Create";
        public const string Edit = "Permissions.Schools.Edit";
        public const string Delete = "Permissions.Schools.Delete";
        public const string Search = "Permissions.Schools.Search";
        public const string Export = "Permissions.Schools.Export";
        public const string Import = "Permissions.Schools.Import";
    }
    [DisplayName("Buses")]
    [Description("Buses Permissions")]
    public static class Buses
    {
        public const string View = "Permissions.Buses.View";
        public const string Create = "Permissions.Buses.Create";
        public const string Edit = "Permissions.Buses.Edit";
        public const string Delete = "Permissions.Buses.Delete";
        public const string Search = "Permissions.Buses.Search";
        public const string Export = "Permissions.Buses.Export";
        public const string Import = "Permissions.Buses.Import";
    }
    [DisplayName("Pilots")]
    [Description("Pilots Permissions")]
    public static class Pilots
    {
        public const string View = "Permissions.Pilots.View";
        public const string Create = "Permissions.Pilots.Create";
        public const string Edit = "Permissions.Pilots.Edit";
        public const string Delete = "Permissions.Pilots.Delete";
        public const string Search = "Permissions.Pilots.Search";
        public const string Export = "Permissions.Pilots.Export";
        public const string Import = "Permissions.Pilots.Import";
    }
    [DisplayName("Parents")]
    [Description("Parents Permissions")]
    public static class Parents
    {
        public const string View = "Permissions.Parents.View";
        public const string Create = "Permissions.Parents.Create";
        public const string Edit = "Permissions.Parents.Edit";
        public const string Delete = "Permissions.Parents.Delete";
        public const string Search = "Permissions.Parents.Search";
        public const string Export = "Permissions.Parents.Export";
        public const string Import = "Permissions.Parents.Import";
    }
    [DisplayName("Students")]
    [Description("Students Permissions")]
    public static class Students
    {
        public const string View = "Permissions.Students.View";
        public const string Create = "Permissions.Students.Create";
        public const string Edit = "Permissions.Students.Edit";
        public const string Delete = "Permissions.Students.Delete";
        public const string Search = "Permissions.Students.Search";
        public const string Export = "Permissions.Students.Export";
        public const string Import = "Permissions.Students.Import";
    }
    [DisplayName("Itineraries")]
    [Description("Itineraries Permissions")]
    public static class Itineraries
    {
        public const string View = "Permissions.Itineraries.View";
        public const string Create = "Permissions.Itineraries.Create";
        public const string Edit = "Permissions.Itineraries.Edit";
        public const string Delete = "Permissions.Itineraries.Delete";
        public const string Search = "Permissions.Itineraries.Search";
        public const string Export = "Permissions.Itineraries.Export";
        public const string Import = "Permissions.Itineraries.Import";
    }
    [DisplayName("TransportLogs")]
    [Description("TransportLogs Permissions")]
    public static class TransportLogs
    {
        public const string View = "Permissions.TransportLogs.View";
        public const string Create = "Permissions.TransportLogs.Create";
        public const string Edit = "Permissions.TransportLogs.Edit";
        public const string Delete = "Permissions.TransportLogs.Delete";
        public const string Search = "Permissions.TransportLogs.Search";
        public const string Export = "Permissions.TransportLogs.Export";
        public const string Import = "Permissions.TransportLogs.Import";
    }
    [DisplayName("Messages")]
    [Description("Messages Permissions")]
    public static class Messages
    {
        public const string View = "Permissions.Messages.View";
        public const string Create = "Permissions.Messages.Create";
        public const string Edit = "Permissions.Messages.Edit";
        public const string Delete = "Permissions.Messages.Delete";
        public const string Search = "Permissions.Messages.Search";
        public const string Export = "Permissions.Messages.Export";
        public const string Import = "Permissions.Messages.Import";
    }
    [DisplayName("AuditTrails")]
    [Description("AuditTrails Permissions")]
    public static class AuditTrails
    {
        public const string View = "Permissions.AuditTrails.View";
        public const string Search = "Permissions.AuditTrails.Search";
        public const string Export = "Permissions.AuditTrails.Export";
    }
    [DisplayName("Logs")]
    [Description("Logs Permissions")]
    public static class Logs
    {
        public const string View = "Permissions.Logs.View";
        public const string Search = "Permissions.Logs.Search";
        public const string Export = "Permissions.Logs.Export";
        public const string Purge = "Permissions.Logs.Purge";
    }
    

    [DisplayName("Products")]
    [Description("Products Permissions")]
    public static class Products
    {
        public const string View = "Permissions.Products.View";
        public const string Create = "Permissions.Products.Create";
        public const string Edit = "Permissions.Products.Edit";
        public const string Delete = "Permissions.Products.Delete";
        public const string Search = "Permissions.Products.Search";
        public const string Export = "Permissions.Products.Export";
        public const string Import = "Permissions.Products.Import";
    }
    [DisplayName("Customers")]
    [Description("Customers Permissions")]
    public static class Customers
    {
        public const string View = "Permissions.Customers.View";
        public const string Create = "Permissions.Customers.Create";
        public const string Edit = "Permissions.Customers.Edit";
        public const string Delete = "Permissions.Customers.Delete";
        public const string Search = "Permissions.Customers.Search";
        public const string Export = "Permissions.Customers.Export";
        public const string Import = "Permissions.Customers.Import";
    }

    [DisplayName("Categories")]
    [Description("Categories Permissions")]
    public static class Categories
    {
        public const string View = "Permissions.Categories.View";
        public const string Create = "Permissions.Categories.Create";
        public const string Edit = "Permissions.Categories.Edit";
        public const string Delete = "Permissions.Categories.Delete";
        public const string Search = "Permissions.Categories.Search";
        public const string Export = "Permissions.Categories.Export";
        public const string Import = "Permissions.Categories.Import";
    }

    [DisplayName("Documents")]
    [Description("Documents Permissions")]
    public static class Documents
    {
        public const string View = "Permissions.Documents.View";
        public const string Create = "Permissions.Documents.Create";
        public const string Edit = "Permissions.Documents.Edit";
        public const string Delete = "Permissions.Documents.Delete";
        public const string Search = "Permissions.Documents.Search";
        public const string Export = "Permissions.Documents.Export";
        public const string Import = "Permissions.Documents.Import";
        public const string Download = "Permissions.Documents.Download";
    }
    [DisplayName("Dictionaries")]
    [Description("Dictionaries Permissions")]
    public static class Dictionaries
    {
        public const string View = "Permissions.Dictionaries.View";
        public const string Create = "Permissions.Dictionaries.Create";
        public const string Edit = "Permissions.Dictionaries.Edit";
        public const string Delete = "Permissions.Dictionaries.Delete";
        public const string Search = "Permissions.Dictionaries.Search";
        public const string Export = "Permissions.Dictionaries.Export";
        public const string Import = "Permissions.Dictionaries.Import";
    }

    [DisplayName("Users")]
    [Description("Users Permissions")]
    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
        public const string Search = "Permissions.Users.Search";
        public const string Import = "Permissions.Users.Import";
        public const string Export = "Permissions.Dictionaries.Export";
        public const string ManageRoles = "Permissions.Users.ManageRoles";
        public const string RestPassword = "Permissions.Users.RestPassword";
        public const string Active = "Permissions.Users.Active";
        public const string ManagePermissions = "Permissions.Users.Permissions";
    }

    [DisplayName("Roles")]
    [Description("Roles Permissions")]
    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
        public const string Search = "Permissions.Roles.Search";
        public const string Export = "Permissions.Roles.Export";
        public const string Import = "Permissions.Roles.Import";
        public const string ManagePermissions = "Permissions.Roles.Permissions";
        public const string ManageNavigation = "Permissions.Roles.Navigation";
    }
    [DisplayName("Multi-Tenant")]
    [Description("Multi-Tenant Permissions")]
    public static class Tenants
    {
        public const string View = "Permissions.Tenants.View";
        public const string Create = "Permissions.Tenants.Create";
        public const string Edit = "Permissions.Tenants.Edit";
        public const string Delete = "Permissions.Tenants.Delete";
        public const string Search = "Permissions.Tenants.Search";
    }
    [DisplayName("Role Claims")]
    [Description("Role Claims Permissions")]
    public static class RoleClaims
    {
        public const string View = "Permissions.RoleClaims.View";
        public const string Create = "Permissions.RoleClaims.Create";
        public const string Edit = "Permissions.RoleClaims.Edit";
        public const string Delete = "Permissions.RoleClaims.Delete";
        public const string Search = "Permissions.RoleClaims.Search";
    }



    [DisplayName("Dashboards")]
    [Description("Dashboards Permissions")]
    public static class Dashboards
    {
        public const string View = "Permissions.Dashboards.View";
    }

    [DisplayName("Hangfire")]
    [Description("Hangfire Permissions")]
    public static class Hangfire
    {
        public const string View = "Permissions.Hangfire.View";
        public const string Jobs = "Permissions.Hangfire.Jobs";
    }


    /// <summary>
    /// Returns a list of Permissions.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetRegisteredPermissions()
    {
        var permissions = new List<string>();
        foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
        {
            var propertyValue = prop.GetValue(null);
            if (propertyValue is not null)
                permissions.Add((string)propertyValue);
        }
        return permissions;
    }


}

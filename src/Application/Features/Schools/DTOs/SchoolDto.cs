// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Schools.DTOs;

[Description("Schools")]
public class SchoolDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = null!;
    [Description("Address")]
    public string? Address { get; set; }
    [Description("Contact")]
    public string? Contact { get; set; }
    [Description("Phone")]
    public string? Phone { get; set; }
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;
    //public  List<StudentDto> Students { get; set; } = new List<StudentDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<School, SchoolDto>().ReverseMap();
        }
    }
}


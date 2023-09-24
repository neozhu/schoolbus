// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Buses.DTOs;

[Description("Buses")]
public class BusDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Plat Number")]
    public string? PlatNumber {get;set;} 
    [Description("Device Id")]
    public string? DeviceId {get;set;} 
    [Description("Device Status")]
    public string? Status {get;set;} 
    [Description("Description")]
    public string? Description {get;set;}
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Bus, BusDto>().ReverseMap();
        }
    }
}


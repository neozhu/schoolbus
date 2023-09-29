// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;

[Description("TransportLogs")]
public class TransportLogDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Student Id")]
    public int StudentId {get;set;}
    [Description("Student")]
    public StudentDto Student { get; set; } = new();
    [Description("Itinerary Id")]
    public int ItineraryId {get;set;}
    [Description("Itinerary")]
    public ItineraryDto Itinerary { get; set; } = new();
    [Description("Device Id")]
    public string? DeviceId {get;set;} 
    [Description("Swipe Date Time")]
    public DateTime SwipeDateTime {get;set;} 
    [Description("Location")]
    public string? Location {get;set;} 
    [Description("Longitude")]
    public double? Longitude {get;set;} 
    [Description("Latitude")]
    public double? Latitude {get;set;} 
    [Description("Check Type")]
    public string? CheckType {get;set;} 
    [Description("Up Or Down")]
    public string? UpOrDown {get;set;} 
    [Description("Comments")]
    public string? Comments {get;set;}
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;
    [Description("Done")]
    public bool Done { get; set; }
    [Description("Reference Id")]
    public int? RefId { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TransportLog, TransportLogDto>().ReverseMap();
        }
    }
}


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Identity.Dto;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;

[Description("Itineraries")]
public class ItineraryDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name {get;set;} = String.Empty; 
    [Description("Description")]
    public string? Description {get;set;}
    [Description("Vehicle Number \\ ID")]
    public int? BusId {get;set;}
    [Description("Driver Id")]
    public string? DriverId { get; set; }
    [Description("Driver")]
    public ApplicationUserDto? Driver { get; set; } = new();
    [Description("Bus")]
    public BusDto Bus { get; set; } = new();
    [Description("Pilot Id")]
    public int PilotId {get;set;}
    [Description("Pilot")]
    public PilotDto Pilot { get; set; } = new();
    [Description("School Id")]
    public int SchoolId {get;set;}
    [Description("School")]
    public SchoolDto School { get; set; } = new();
    [Description("First Time")]
    public string? FirstTime {get;set;} 
    [Description("Last Time")]
    public string? LastTime {get;set;} 
    [Description("Starting Station")]
    public string? StartingStation {get;set;} 
    [Description("Terminal Station")]
    public string? TerminalStation {get;set;} 
    [Description("Disabled")]
    public bool Disabled {get;set;}
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;

    //public List<TransportLogDto> TransportLogs { get; set; }= new List<TransportLogDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Itinerary, ItineraryDto>().ReverseMap();
        }
    }
}


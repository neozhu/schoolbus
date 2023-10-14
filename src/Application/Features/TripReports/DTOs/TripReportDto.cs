// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using AutoMapper.Configuration.Conventions;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Domain.Common;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;

[Description("TripReports")]
public class TripReportDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Itinerary Id")]
    public int? ItineraryId { get; set; }
    [Description("Itinerary")]
    public  ItineraryDto? Itinerary { get; set; }
    [Description("Pilot Id")]
    public int? PilotId { get; set; }
    [Description("Pilot")]
    public PilotDto? Pilot { get; set; }

    [Description("Plat Number")]
    public string? PlatNumber { get; set; }
    [Description("On Board")]
    public int OnBoard { get; set; }
    [Description("Not On Board")]
    public int NotOnBoard { get; set; }
    [Description("Departure Date")]
    public DateTime? DepartureDate { get; set; }
    [Description("Report Date")]
    public DateTime? ReportDate { get; set; }
    [Description("Status")]
    public TripStatus Status { get; set; } = TripStatus.Runing;
    [Description("Comments")]
    public string? Comments { get; set; }
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;
    [Description("Trip Logs")]
    public List<TripLogDto> TripLogs { get; set; } = new List<TripLogDto>();
    [Description("Accidents")]
    public List<TripAccidentDto> TripAccidents { get; set; } = new List<TripAccidentDto>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripReport, TripReportDto>().ReverseMap();
        }
    }
}
public class TripReportToPlainDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Itinerary Id")]
    public int? ItineraryId { get; set; }
    [Description("Itinerary")]
    public ItineraryDto? Itinerary { get; set; }
    [Description("Pilot Id")]
    public int? PilotId { get; set; }
    [Description("Pilot")]
    public PilotDto? Pilot { get; set; }

    [Description("Plat Number")]
    public string? PlatNumber { get; set; }
    [Description("On Board")]
    public int OnBoard { get; set; }
    [Description("Not On Board")]
    public int NotOnBoard { get; set; }
    [Description("Departure Date")]
    public DateTime? DepartureDate { get; set; }
    [Description("Report Date")]
    public DateTime? ReportDate { get; set; }
    [Description("Status")]
    public TripStatus Status { get; set; } = TripStatus.Runing;
    [Description("Comments")]
    public string? Comments { get; set; }
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripReport, TripReportToPlainDto>().ReverseMap();
        }
    }
}


[Description("TripAccidents")]
public class TripLogDto  
{
    public int Id { get; set; }
    public int? StudentId { get; set; }
    public string? UID { get; set; }
    public StudentDto Student { get; set; } = null!;
    public int TripId { get; set; }
   // public TripReportDto TripReport { get; set; } = null!;
    public DateTime? GetOnDateTime { get; set; }
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public bool Manual { get; set; }
    public string? Comments { get; set; }
    public DateTime? GetOffDateTime2 { get; set; }
    public string? Location2 { get; set; }
    public double? Longitude2 { get; set; }
    public double? Latitude2 { get; set; }
    public string? Comments2 { get; set; }
    public bool Manual2 { get; set; }

    public string TenantId { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripLog, TripLogDto>().ReverseMap();
        }
    }
}


[Description("TripAccidents")]
public class TripAccidentDto
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public virtual TripReportDto TripReport { get; set; } = null!;
    public DateTime? ReportDateTime { get; set; }
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public AccidentLevel Level { get; set; } = AccidentLevel.Trouble;
    public string? Comments { get; set; }
    public string? Status { get; set; }
    public string? Responder { get; set; }
    public string? Result { get; set; }
    public string TenantId { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripAccident, TripAccidentDto>().ReverseMap();
        }
    }
}
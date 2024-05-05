// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace CleanArchitecture.Blazor.Application.Features.Students.DTOs;

[Description("Students")]
public class StudentDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("UID")]
    public string? UID { get; set; }
    [Description("Last Name")]
    public string? LastName { get; set; }
    [Description("First Name")]
    public string? FirstName { get; set; }
    [Description("Display Name")]
    public string? DisplayName => $"{LastName} {FirstName}";
    [Description("Profile Picture")]
    public string? ProfilePicture { get; set; }
    [Description("Phone")]
    public string? Phone { get; set; }
    [Description("Description")]
    public string? Description { get; set; }
    [Description("Status")]
    public string? Status { get; set; }
    [Description("School Id")]
    public int SchoolId { get; set; }

    [Description("School")]
    public SchoolDto School { get; set; } = new();

    [Description("Assigned Itinerary Id")]
    public int? ItineraryId { get; set; }

    [Description("Assigned Itinerary")]
    public ItineraryDto? Itinerary { get; set; }


    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;
    [Description("Grade")]
    public string? Grade { get; set; }

    public List<ParentDto> Parents { get; set; } = new List<ParentDto>();
    public List<TransportLogDto> TransportLogs { get; set; } = new List<TransportLogDto>();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Student, StudentDto>(MemberList.None).ForMember(x=>x.Parents,y=>y.Ignore()).ForMember(x=>x.TransportLogs,y=>y.Ignore());
            CreateMap<StudentDto,Student>(MemberList.None).ForMember(x => x.Parents, y => y.Ignore()).ForMember(x => x.TransportLogs, y => y.Ignore()).ForMember(x=>x.School,y=>y.Ignore());
        }
    }
}


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;
using DocumentFormat.OpenXml.Bibliography;

namespace CleanArchitecture.Blazor.Application.Features.Parents.DTOs;

[Description("Parents")]
public class ParentDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Last Name")]
    public string? LastName { get; set; }
    [Description("First Name")]
    public string? FirstName { get; set; }
    [Description("Display Name")]
    public string? DisplayName => $"{LastName} {FirstName}";
    [Description("Profile Picture")]
    public string? ProfilePicture {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Status")]
    public string? Status {get;set;}
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;

    public List<StudentDto> Children { get; set; } = new List<StudentDto>();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Parent, ParentDto>(MemberList.None).ReverseMap();
        }
    }
}


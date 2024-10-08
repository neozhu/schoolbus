﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Tenants.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Messages.DTOs;

[Description("Messages")]
public class MessageDto
{
    [Description("Id")]
    public int Id { get; set; }
        [Description("From")]
    public string? From {get;set;} 
    [Description("To")]
    public string? To {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Content")]
    public string? Content {get;set;} 
    [Description("Sent Time")]
    public DateTime? SentTime {get;set;} 
    [Description("Read Time")]
    public DateTime? ReadTime {get;set;} 
    [Description("Status")]
    public string? Status {get;set;}
    [Description("Organization Id")]
    public string TenantId { get; set; } = null!;
    [Description("Organization")]
    public TenantDto Tenant { get; set; } = null!;


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}


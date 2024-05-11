﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Domain.Enums;

namespace CleanArchitecture.Blazor.Domain.Entities;
public  class TripAccident : BaseAuditableEntity, IMustHaveTenant
{
    public int TripId { get; set; }
    public virtual TripReport TripReport { get; set; } = null!;
    public DateTime? ReportDateTime { get; set; } 
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public AccidentLevel Level { get; set; } = AccidentLevel.Trouble;
    public InfractionType? Infraction { get; set; }
    public string? Comments { get; set; }
    public string? Status { get; set; }
    public string? Responder { get; set; }
    public string? Result { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
    public int? StudentId { get; set; }
    public virtual Student? Student { get; set; }
}

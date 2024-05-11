﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Bus : BaseAuditableEntity, IMustHaveTenant
{
    public string PlatNumber { get; set; } = string.Empty;

    public string? Type { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? DeviceId { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<Itinerary> Itineraries { get; set; } = new HashSet<Itinerary>();
}

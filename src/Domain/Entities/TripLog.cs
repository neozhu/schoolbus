using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public  class TripLog: BaseAuditableEntity, IMustHaveTenant
{
    public int? StudentId { get; set; }
    public string? UID { get; set; }
    public virtual Student? Student { get; set; } 
    public int TripId { get; set; }
    public virtual TripReport TripReport { get; set; } = null!;
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
    public virtual Tenant Tenant { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public  class TransportLog: BaseAuditableEntity, IMustHaveTenant
{
    public int StudentId { get; set; }
    public virtual Student Student { get; set; } = null!;
    public int ItineraryId { get; set; }
    public virtual Itinerary Itinerary { get; set; } = null!;
    public string? DeviceId { get; set; }
    public DateTime SwipeDateTime { get; set; } = DateTime.Now;
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? CheckType { get; set; }
    public string? UpOrDown { get; set; }
    public string? Comments { get; set; }
    public int? RefId { get; set; }
    public bool Done { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class TripReport: BaseAuditableEntity, IMustHaveTenant
{
    public int? ItineraryId { get; set; }
    public virtual Itinerary? Itinerary { get; set; }
    public int? PilotId { get; set; }
    public virtual Pilot? Pilot { get; set; }
    public string PlatNumber { get; set; } = string.Empty;
    public int OnBoard { get; set; }
    public int NotOnBoard { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ReportDate { get; set; }
    public string? Comments { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<TripLog> TripLogs { get; set; } = new HashSet<TripLog>();
    public virtual ICollection<TripAccident> TripAccidents { get; set; } = new HashSet<TripAccident>();
}

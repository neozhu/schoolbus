using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Pilot : BaseAuditableEntity, IMustHaveTenant
{
    public string LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? ProfilePicture { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<Itinerary> Itineraries { get; set; } = new HashSet<Itinerary>();
}

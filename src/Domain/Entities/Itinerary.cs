using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Itinerary:BaseAuditableEntity, IMustHaveTenant
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int BusId { get; set; }
    public virtual Bus Bus { get; set; } = null!;
    public int PilotId { get; set; }
    public virtual Pilot Pilot { get; set; } = null!;
    public int SchoolId { get; set; }
    public virtual School School { get; set; } = null!;
    public string FirstTime { get; set; } = null!;
    public string LastTime { get; set; } = null!;
    public string StartingStation { get; set; } = null!;
    public string TerminalStation { get; set; } = null!;
    public bool Disabled { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<TransportLog> TransportLogs { get; set; } = new HashSet<TransportLog>();

    public virtual ICollection<Student> Students { get; set; } =    new HashSet<Student>();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Student : BaseAuditableEntity, IMustHaveTenant
{
    public string UID { get; set; } = null!;
    public string LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Phone { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public virtual ICollection<Parent> Parents { get; set; } = new HashSet<Parent>();
    public int SchoolId { get; set; }
    public virtual School School { get; set; } = null!;
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<TransportLog>  TransportLogs{ get; set; } = new HashSet<TransportLog>();


}

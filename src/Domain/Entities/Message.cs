using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class Message : BaseAuditableEntity, IMustHaveTenant
{
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Phone { get; set; }
    public string? Content { get; set; }
    public DateTime? SentTime { get; set; }
    public DateTime? ReadTime { get; set; }
    public string? Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public virtual Tenant Tenant { get; set; } = null!;
}

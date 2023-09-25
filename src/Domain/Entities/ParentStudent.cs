using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Entities;
public class ParentStudent
{
    public int ChildrenId { get; set; }
    public virtual Student? Student { get; set; } 
    public int ParentsId { get; set; }
    public virtual Parent? Parent { get; set; } 
}

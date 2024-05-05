using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Enums;
public enum AccidentLevel
{
    [Description("Hight")]
    Hight,
    [Description("Trouble")]
    Trouble
 
}


public enum InfractionType
{
    [Description("Moving around while bus in motion")]
    Moving_around_while_bus_in_motion,
    [Description("Leaving assigned seat")]
    Leaving_assigned_seat,
    [Description("Throw object/garbage")]
    Throw_object_garbage,
    [Description("Others")]
    Others

}
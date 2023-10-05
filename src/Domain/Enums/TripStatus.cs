using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Domain.Enums;
public enum TripStatus
{
    [Description("Runing")]
    Runing,
    [Description("Suspend")]
    Suspend,
    [Description("Done")]
    Done
}

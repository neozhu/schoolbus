using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Features.Dashboard.Dto;
public record SummaryDto
{
    public int TotalSchools { get; set; }
    public int TotalBuses { get; set; }
    public int TotalItineraries { get; set; }
    public int TotalParents { get; set; }
    public int TotalPilots { get; set; }
    public int TotalStudents { get; set; }
    public int TotalTransportLogs { get; set; }
}

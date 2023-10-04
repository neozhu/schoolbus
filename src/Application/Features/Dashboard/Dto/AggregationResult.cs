using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Features.Dashboard.Dto;


public record AggregationResult {
    public SummaryDto Summary { get; set; }
    public List<TotalOfMonthDto> TotalOfMonth { get; set; }
    public List<TotalOfStudentDto> TotalOfStudent { get; set; }
    public List<TotalOfTransportDto> TotalOfTransport { get; set; }
}


public record SummaryDto
{
    public int TotalSchools { get; set; }
    public int TotalBuses { get; set; }
    public int TotalItineraries { get; set; }
    public int TotalParents { get; set; }
    public int TotalPilots { get; set; }
    public int TotalStudents { get; set; }
    public int TotalTransportLogs { get; set; }
    public int TotalReports { get; set; }
}
public record TotalOfMonthDto
{
    public string YearMonth { get; set; } = string.Empty;
    public int Count { get; set; }
}

public record TotalOfStudentDto
{
    public string SchoolName { get; set; } = string.Empty;
    public int Count { get; set; }
}
public record TotalOfTransportDto
{
    public string ItinerayName { get; set; } = string.Empty;
    public int Count { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Features.Dashboard.Dto;
public record TotalOfMonthDto
{
    public string YearMonth { get; set; } = string.Empty;
    public int Count { get; set; }
}

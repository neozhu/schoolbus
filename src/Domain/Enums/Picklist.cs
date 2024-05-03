// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;

namespace CleanArchitecture.Blazor.Domain.Enums;

public enum Picklist
{
    [Description("Infraction")]
    Infraction,
    [Description("Location")]
    Location,
    [Description("Status")]
    Status,
    [Description("Unit")]
    Unit,
    [Description("Brand")]
    Brand
}

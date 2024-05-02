// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class TripReportConfiguration : IEntityTypeConfiguration<TripReport>
{
    public void Configure(EntityTypeBuilder<TripReport> builder)
    {
        builder.Property(t => t.Status).HasConversion<string>();
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.HasOne(t => t.Itinerary).WithMany().HasForeignKey(x => x.ItineraryId);
        builder.HasOne(t => t.Pilot).WithMany().HasForeignKey(x => x.PilotId);
        builder.HasMany(t => t.TripLogs).WithOne(x => x.TripReport).HasForeignKey(x => x.TripId);
        builder.HasMany(t => t.TripAccidents).WithOne(x => x.TripReport).HasForeignKey(x => x.TripId);
        builder.Navigation(e => e.Tenant).AutoInclude();
        builder.Navigation(e => e.Itinerary).AutoInclude();
        //builder.Navigation(e => e.Pilot).AutoInclude();
        builder.HasOne(e => e.Driver).WithMany().HasForeignKey(x => x.DriverId);
        builder.Navigation(e => e.Driver).AutoInclude();
        builder.Ignore(e => e.DomainEvents);
    }
}



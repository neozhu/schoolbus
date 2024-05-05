// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class ItineraryConfiguration : IEntityTypeConfiguration<Itinerary>
{
    public void Configure(EntityTypeBuilder<Itinerary> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.Navigation(e => e.Tenant).AutoInclude();
        builder.HasOne(t => t.School).WithMany(x=>x.Itineraries).HasForeignKey(x => x.SchoolId);
        builder.Navigation(e => e.School).AutoInclude();
        builder.HasOne(t => t.Bus).WithMany(x => x.Itineraries).HasForeignKey(x => x.BusId);
        builder.Navigation(e => e.Bus).AutoInclude();
        builder.HasOne(t => t.Pilot).WithMany(x => x.Itineraries).HasForeignKey(x => x.PilotId);
        builder.Navigation(e => e.Pilot).AutoInclude();
        builder.HasOne(e=>e.Driver).WithMany().HasForeignKey(x=>x.DriverId);
        builder.Navigation(e => e.Driver).AutoInclude();

        builder.HasMany(x => x.Students).WithOne(x => x.Itinerary).HasForeignKey(x => x.ItineraryId);
        builder.Ignore(e => e.DomainEvents);
    }
}



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
        builder.HasOne(t => t.School).WithMany().HasForeignKey(x => x.SchoolId).IsRequired(); 
        builder.Navigation(e => e.School).AutoInclude();
        builder.HasOne(t => t.Bus).WithMany().HasForeignKey(x => x.BusId).IsRequired(); 
        builder.Navigation(e => e.Bus).AutoInclude();
        builder.HasOne(t => t.Pilot).WithMany().HasForeignKey(x => x.PilotId).IsRequired(); 
        builder.Navigation(e => e.Pilot).AutoInclude();
        builder.HasMany(t => t.TransportLogs).WithOne(x => x.Itinerary).HasForeignKey(x => x.ItineraryId).IsRequired();
        builder.Ignore(e => e.DomainEvents);
    }
}



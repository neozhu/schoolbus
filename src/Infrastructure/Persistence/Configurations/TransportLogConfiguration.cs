﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class TransportLogConfiguration : IEntityTypeConfiguration<TransportLog>
{
    public void Configure(EntityTypeBuilder<TransportLog> builder)
    {
        builder.Property(x => x.Latitude).HasPrecision(16, 10);
        builder.Property(x => x.Longitude).HasPrecision(16, 10);
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.HasOne(t => t.Itinerary).WithMany(x=>x.TransportLogs).HasForeignKey(x => x.ItineraryId);
        builder.HasOne(t => t.Student).WithMany(x => x.TransportLogs).HasForeignKey(x => x.StudentId);
        builder.Ignore(e => e.DomainEvents);
    }
}



// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class TripAccidentConfiguration : IEntityTypeConfiguration<TripAccident>
{
    public void Configure(EntityTypeBuilder<TripAccident> builder)
    {
        builder.Property(t => t.Level).HasConversion<string>();
        builder.Property(t => t.Infraction).HasConversion<string>();
        builder.Property(x => x.Latitude).HasPrecision(16, 10);
        builder.Property(x => x.Longitude).HasPrecision(16, 10);
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.HasOne(t => t.Student).WithMany().HasForeignKey(x => x.StudentId);
        builder.Ignore(e => e.DomainEvents);
    }
}



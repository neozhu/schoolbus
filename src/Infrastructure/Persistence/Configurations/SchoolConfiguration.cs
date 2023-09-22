// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.Navigation(e => e.Tenant).AutoInclude();
        builder.HasMany(t => t.Students).WithOne(t => t.School).HasForeignKey(t => t.SchoolId).IsRequired();
        builder.HasMany(t => t.Itineraries).WithOne(t => t.School).HasForeignKey(t => t.SchoolId).IsRequired();
        builder.Ignore(e => e.DomainEvents);
    }
}



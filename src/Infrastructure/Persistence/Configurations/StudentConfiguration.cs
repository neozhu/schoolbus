// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(t => t.LastName).HasMaxLength(50).IsRequired();
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.HasOne(t => t.School).WithMany(x=>x.Students).HasForeignKey(x => x.SchoolId);
        builder.Navigation(e => e.Tenant).AutoInclude();
        builder.Navigation(e => e.School).AutoInclude();
        builder.Ignore(e => e.DomainEvents);
    }
}



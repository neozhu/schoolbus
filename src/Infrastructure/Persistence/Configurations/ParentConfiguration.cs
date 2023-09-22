// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using CleanArchitecture.Blazor.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.Property(t => t.LastName).HasMaxLength(50).IsRequired();
        builder.HasOne(t => t.Tenant).WithMany().HasForeignKey(x => x.TenantId);
        builder.Navigation(e => e.Tenant).AutoInclude();
        builder.HasMany(t => t.Children).WithMany(x => x.Parents);
       
        builder.Ignore(e => e.DomainEvents);
    }
}



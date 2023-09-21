// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Blazor.Domain.Entities.Logger;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchitecture.Blazor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Logger> Loggers { get; set; }
    DbSet<AuditTrail> AuditTrails { get; set; }
    DbSet<Document> Documents { get; set; }
    DbSet<KeyValue> KeyValues { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    DbSet<Customer> Customers { get; set; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<School> Schools { get; set; }
    DbSet<Bus> Buses { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<Parent> Parents { get; set; }
    DbSet<Pilot> Pilots { get; set; }
    DbSet<Itinerary> Itineraries { get; set; }
    DbSet<TransportLog> TransportLogs { get; set; }
    DbSet<Message> Messages { get; set; }
}

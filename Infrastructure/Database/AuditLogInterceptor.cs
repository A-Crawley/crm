using System.Text.Json;
using Domain.Interfaces;
using Infrastructure.BackgroundWorkers;
using Infrastructure.Database.Entities;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database;

public class AuditLogInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;
    private readonly AuditLogQueue _auditLogQueue;

    public AuditLogInterceptor(IDateTimeService dateTimeService, AuditLogQueue auditLogQueue)
    {
        _dateTimeService = dateTimeService;
        _auditLogQueue = auditLogQueue;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        HandlePost(eventData);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        HandlePost(eventData);
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void HandlePost(DbContextEventData eventData)
    {
        foreach (var entityEntry in eventData.Context?.ChangeTracker.Entries() ?? [])
        {
            if (entityEntry.Entity is not IEntity entity) continue;

            var newVal = JsonSerializer.Serialize(entityEntry.CurrentValues.ToObject());
            var oldVal = JsonSerializer.Serialize(entityEntry.OriginalValues.ToObject());

            var audit = new AuditLog
            {
                Reference = entity.Id,
                Table = entityEntry.Entity.GetType().Name,
                OldValue = oldVal == newVal ? null : oldVal,
                NewValue = newVal,
                When = _dateTimeService.Now
            };
            _auditLogQueue.Add(audit);
        }

    }
}
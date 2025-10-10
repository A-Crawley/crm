using Domain.Interfaces;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database;

public class AuditableColumnsInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;

    public AuditableColumnsInterceptor(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Handle(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        Handle(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void Handle(DbContextEventData eventData)
    {
        foreach (var entityEntry in eventData.Context?.ChangeTracker.Entries() ?? [])
        {
            if (entityEntry.Entity is not IEntity entity) continue;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = _dateTimeService.Now;
                    continue;
                case EntityState.Modified:
                    entity.UpdatedAt = _dateTimeService.Now;
                    continue;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entity.ArchivedAt = _dateTimeService.Now;
                    continue;
            }
        }
    }
}
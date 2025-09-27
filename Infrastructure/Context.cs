using Domain.Interfaces;
using Infrastructure.Database.Entities;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class AuditableInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;

    public AuditableInterceptor(IDateTimeService dateTimeService)
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

public interface IContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task AddAsync(IEntity entity, CancellationToken cancellationToken);
    void Update(IEntity entity);
    void Delete(IEntity entity);
}

public class Context : DbContext, IContext
{
    private readonly AuditableInterceptor _auditableInterceptor;
    public DbSet<User> Users { get; set; }
    
    public Context(DbContextOptions<Context> options, AuditableInterceptor auditableInterceptor) : base(options)
    {
        _auditableInterceptor = auditableInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);   
        base.OnModelCreating(modelBuilder);
    }

    public async Task AddAsync(IEntity entity, CancellationToken cancellationToken)
    {
        await base.AddAsync(entity, cancellationToken);
    }

    public void Update(IEntity entity)
    {
        base.Update(entity);
    }

    public void Delete(IEntity entity)
    {
        base.Remove(entity);
    }
}
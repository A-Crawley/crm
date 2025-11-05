using Infrastructure.Database.Entities;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class Context : DbContext, IContext
{
    private readonly AuditableColumnsInterceptor _auditableColumnsInterceptor;
    private readonly AuditLogInterceptor _auditLogInterceptor;
    public DbSet<User> Users { get; set; }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<AuditLog> AuditLog { get; set; }

    public Context(
        DbContextOptions<Context> options,
        AuditableColumnsInterceptor auditableColumnsInterceptor,
        AuditLogInterceptor auditLogInterceptor) : base(options)
    {
        _auditableColumnsInterceptor = auditableColumnsInterceptor;
        _auditLogInterceptor = auditLogInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableColumnsInterceptor, _auditLogInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task AddAuditAsync(AuditLog auditLog, CancellationToken cancellationToken)
    {
        await base.AddAsync(auditLog, cancellationToken);
        await SaveChangesAsync(cancellationToken);
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
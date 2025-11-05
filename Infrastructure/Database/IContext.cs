using Infrastructure.Database.Entities;
using Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public interface IContext
{
    DbSet<User> Users { get; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task AddAuditAsync(AuditLog auditLog, CancellationToken cancellationToken);
    Task AddAsync(IEntity entity, CancellationToken cancellationToken);
    void Update(IEntity entity);
    void Delete(IEntity entity);
}
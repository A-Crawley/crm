using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public abstract class RepositoryBase<TEntity, TDto> where TEntity : Entity
    where TDto : class
{
    protected readonly IContext Context;

    protected RepositoryBase(IContext context)
    {
        Context = context;
    }

    public async Task<TDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Map(Context.Set<TEntity>().Where(x => x.Id == id)).FirstOrDefaultAsync(cancellationToken);
    }

    protected abstract IQueryable<TDto> Map(IQueryable<TEntity> queryable);
}
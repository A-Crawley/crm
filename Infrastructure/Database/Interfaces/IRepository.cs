namespace Infrastructure.Database.Interfaces;

public interface IRepository<TDto>
    where TDto : class
{
    Task<TDto?> GetAsync(int id, CancellationToken cancellationToken = default);
}
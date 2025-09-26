using Domain.DTOs;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public interface IRepository;

public interface IUserRepository : IRepository
{
    Task<UserDto?> GetAsync(int id, CancellationToken cancellationToken = default);
}

public class UserRepository : IUserRepository
{
    private readonly IContext _context;

    public UserRepository(IContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await MapToDto(_context.Users.Where(x => x.Id == id)).FirstOrDefaultAsync(cancellationToken);
    }

    private static IQueryable<UserDto> MapToDto(IQueryable<User> query)
    {
        return query.Select(u =>
            new UserDto(
                u.Id,
                u.Email,
                u.PasswordHash,
                new Auditable(u.CreatedBy, u.CreatedAt),
                u.UpdatedAt != null ? new Auditable(u.UpdatedBy, u.UpdatedAt.Value) : null,
                u.ArchivedAt != null ? new Auditable(u.ArchivedBy, u.ArchivedAt.Value) : null
            )
        );
    }
}
using System.ComponentModel.DataAnnotations;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models.User;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public interface IRepository;

public interface IUserRepository : IRepository
{
    Task<UserDto?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> EmailInUse(string email, CancellationToken cancellationToken = default);
    Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
}

public class UserRepository : IUserRepository
{
    private readonly IContext _context;
    private readonly IHashingService _hashingService;

    public UserRepository(IContext context, IHashingService hashingService)
    {
        _context = context;
        _hashingService = hashingService;
    }

    public async Task<UserDto?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await MapToDto(_context.Users.Where(x => x.Id == id)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> EmailInUse(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(x => x.Email == email && x.ArchivedAt == null, cancellationToken);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var passwordHash = _hashingService.Hash(request.Password);
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return (await GetAsync(user.Id, cancellationToken))!;
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
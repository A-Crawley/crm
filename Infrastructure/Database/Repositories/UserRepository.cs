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

    Task<UserDto?> CheckCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    );

    Task AddNewLoginSessionAsync(int userId, string refreshToken, CancellationToken cancellationToken = default);
}

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

public class UserRepository : RepositoryBase<User, UserDto>, IUserRepository
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IHashingService _hashingService;

    public UserRepository(IContext context, IDateTimeService dateTimeService, IHashingService hashingService)
    : base(context)
    {
        _dateTimeService = dateTimeService;
        _hashingService = hashingService;
    }

    public async Task<bool> EmailInUse(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Users.AnyAsync(x => x.Email == email && x.ArchivedAt == null, cancellationToken);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var passwordHash = _hashingService.Hash(request.Password);
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(cancellationToken);
        return (await GetAsync(user.Id, cancellationToken))!;
    }

    public async Task<UserDto?> CheckCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    )
    {
        var passwordHash = _hashingService.Hash(password);
        var user = await Map(
                Context.Users.Where(x => x.Email == email && x.PasswordHash == passwordHash)
                ).SingleOrDefaultAsync(cancellationToken);
        return user;
    }

    public async Task AddNewLoginSessionAsync(int userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        var loginSession = new LoginSession
        {
            UserId = userId,
            RefreshToken = refreshToken,
            Expiry = _dateTimeService.Now.AddDays(7)
        };

        await Context.AddAsync(loginSession, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    protected override IQueryable<UserDto> Map(IQueryable<User> query)
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
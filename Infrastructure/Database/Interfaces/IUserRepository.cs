using Domain.DTOs;
using Domain.Models.User;

namespace Infrastructure.Database.Interfaces;

public interface IUserRepository : IRepository<UserDto>
{
    Task<bool> EmailInUse(string email, CancellationToken cancellationToken = default);
    Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

    Task<UserDto?> CheckCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default
    );

    Task AddNewLoginSessionAsync(int userId, string refreshToken, CancellationToken cancellationToken = default);
}
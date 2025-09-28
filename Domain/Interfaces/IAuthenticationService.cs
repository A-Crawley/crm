using Domain.DTOs;

namespace Domain.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse?> GenerateJwtAsync(int userId, CancellationToken cancellationToken);
}
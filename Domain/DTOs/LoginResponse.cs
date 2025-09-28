namespace Domain.DTOs;

public record LoginResponse(string JwtToken, string RefreshToken);
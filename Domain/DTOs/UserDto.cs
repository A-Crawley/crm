namespace Domain.DTOs;

public record UserDto(int Id, string Email, string PasswordHash, Auditable Created, Auditable? Updated, Auditable? Archived);
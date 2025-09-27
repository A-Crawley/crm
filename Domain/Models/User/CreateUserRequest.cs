using System.Security;

namespace Domain.Models.User;

public record CreateUserRequest(string Email, string Password);
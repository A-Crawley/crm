using Domain.DTOs;

namespace WebApi.Models.Response;

public class UserResponse
{
    public int Id { get; internal set; }
    public string Email { get; internal set; } = string.Empty;

    public static UserResponse FromUser(UserDto user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}
using System.ComponentModel.DataAnnotations;
using Application.Commands.UserCommands;

namespace WebApi.Models.Request;

public class CreateUserRequest
{
    [Required]
    public required string Email { get; init; }
    
    [Required]
    public required string Password { get; init; }
    

    public CreateUserCommandRequest ToCommandRequest()
    {
        return new CreateUserCommandRequest(Email, Password);
    }
}
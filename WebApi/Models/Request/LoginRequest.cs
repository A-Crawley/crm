using Application.Commands.UserCommands;

namespace WebApi.Models.Request;

public record LoginRequest(string Email, string Password)
{
    public LoginCommandRequest ToCommandRequest() => new LoginCommandRequest(Email, Password);
}
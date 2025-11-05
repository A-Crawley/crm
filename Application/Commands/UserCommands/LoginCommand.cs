using Application.Interfaces;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Commands.UserCommands;

public record LoginCommandRequest(string Email, string Password) : ICommandRequest;

public interface ILoginCommand : ICommand<LoginCommandRequest, Result<LoginResponse>>;

public class LoginCommand : Command<LoginCommandRequest, Result<LoginResponse>>, ILoginCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public LoginCommand(
        IUserRepository userRepository,
        IAuthenticationService authenticationService,
        ILogger<LoginCommand> logger
    ) : base(logger)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    protected override async Task<Result<LoginResponse>> CommandAsync(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.CheckCredentialsAsync(request.Email, request.Password, cancellationToken);
        if (user is null) return Error.NotFoundError();

        var loginModel = await _authenticationService.GenerateJwtAsync(user.Id, cancellationToken);
        if (loginModel is null) return Error.GeneralError("Issue generating token");

        await _userRepository.AddNewLoginSessionAsync(user.Id, loginModel.RefreshToken, cancellationToken);
        return loginModel;
    }

    protected override Task EventsAsync(LoginCommandRequest request, Result<LoginResponse> result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
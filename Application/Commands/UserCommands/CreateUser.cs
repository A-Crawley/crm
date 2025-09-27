using Application.Interfaces;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Database.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Commands.UserCommands;

public interface ICreateUserCommand : ICommand<CreateUserCommandRequest, Result<UserDto>>;

public record CreateUserCommandRequest(string Email, string Password) : ICommandRequest;

public class CreateUserCommand : Command<CreateUserCommandRequest, Result<UserDto>>, ICreateUserCommand
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommand(IUserRepository userRepository, ILogger logger) : base(logger)
    {
        _userRepository = userRepository;
    }

    protected override async Task<Result<UserDto>> CommandAsync(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _userRepository.EmailInUse(request.Email, cancellationToken))
            return Error.ConflictError();

        var user = await _userRepository.CreateUserAsync(new(request.Email, request.Password), cancellationToken);
        return user;
    }

    protected override Task EventsAsync(CreateUserCommandRequest request, Result<UserDto> result,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
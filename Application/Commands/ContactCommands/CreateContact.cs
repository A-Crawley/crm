using Application.Interfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ContactCommands;

public record CreateContactRequest() : ICommandRequest;

public interface ICreateContactCommand : ICommand<CreateContactRequest, Result<ContactDto>>;

public class CreateContact : Command<CreateContactRequest, Result<ContactDto>>, ICreateContactCommand
{
    public CreateContact(ILogger logger) : base(logger)
    {
    }

    protected override Task<Result<ContactDto>> CommandAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task EventsAsync(CreateContactRequest request, Result<ContactDto> result, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
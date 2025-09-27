using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Commands;

public abstract class Command<TIn, TOut> : ICommand<TIn, TOut> where TIn: ICommandRequest where TOut: Result
{
    private readonly ILogger _logger;

    protected Command(ILogger logger)
    {
        _logger = logger;
    }
    
    public async Task<TOut> ExecuteAsync(TIn request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing command {commandName}", typeof(TIn).Name);
        var result = await CommandAsync(request, cancellationToken);
        if (result.IsSuccess)
        {
            _logger.LogInformation("Successfully executed command {commandName}", typeof(TIn).Name);
            await EventsAsync(request, result, cancellationToken);
        }
        else
        {
            _logger.LogError("Failed executing command {commandName}: {errorMessage}", typeof(TIn).Name, result.Error);
        }
        
        return result;
    }

    protected abstract Task<TOut> CommandAsync(TIn request, CancellationToken cancellationToken);
    
    protected abstract Task EventsAsync(TIn request, TOut result, CancellationToken cancellationToken);
}
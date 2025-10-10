using Domain.Models;

namespace Application.Interfaces;

public interface IQueryRequest;

public interface ICommandRequest;

public interface ICommand<TIn, TOut> where TIn : ICommandRequest where TOut : Result
{
    Task<TOut> ExecuteAsync(TIn request, CancellationToken cancellationToken);
}
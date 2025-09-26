using Domain.Models;

namespace Application;

public interface IQuery<TIn,TOut> where TIn : IQueryRequest where TOut : Result
{ 
    Task<TOut> ExecuteAsync(TIn request, CancellationToken token);
}
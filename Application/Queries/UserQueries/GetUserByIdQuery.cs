using Application.Interfaces;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories;

namespace Application.Queries.UserQueries;

public record GetUserByIdQueryRequest(int Id) : IQueryRequest;

public interface IGetUserByIdQuery : IQuery<GetUserByIdQueryRequest, Result<UserDto>>;

public class GetUserByIdQuery : IGetUserByIdQuery
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQuery(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> ExecuteAsync(GetUserByIdQueryRequest request, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(request.Id, token);
        if (user is null) return Error.NotFoundError();
        return user;
    }
}
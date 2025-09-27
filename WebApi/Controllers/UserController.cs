using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ApiControllerBase
{
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> Get(
        [FromRoute]int userId,
        [FromServices] IGetUserByIdQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var user = await query.ExecuteAsync(new GetUserByIdQueryRequest(userId), cancellationToken);
        if (user.IsSuccess) return Ok(UserResponse.FromUser(user.Value));
        
        return Error(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateUserRequest request,
        [FromServices] ICreateUserCommand command, 
        CancellationToken cancellationToken = default
    )
    {
        var result = await command.ExecuteAsync(request.ToCommandRequest(), cancellationToken);
        if (result.IsSuccess) return Ok(UserResponse.FromUser(result.Value));
        
        return Error(result);
    }
    
}
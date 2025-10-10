using Application.Commands.UserCommands;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Request;

namespace WebApi.Controllers;

[Route("api/v1/auth")]
public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var loginResponse = await command.ExecuteAsync(request.ToCommandRequest(), cancellationToken);
        if (loginResponse.IsSuccess) return Ok(loginResponse.Value);
        return Error(loginResponse);
    }

}
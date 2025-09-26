using Application;
using Application.Queries.UserQueries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> Get(
        [FromRoute]int userId,
        [FromServices] IGetUserByIdQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var user = await query.ExecuteAsync(new GetUserByIdQueryRequest(userId), cancellationToken);
        if (user.IsSuccess) return Ok(user.Value);
        return NotFound();
    }
}
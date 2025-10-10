using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Response;

namespace WebApi.Models;

public class ApiControllerBase : ControllerBase
{
    public new OkObjectResult Ok()
    {
        return new OkObjectResult(ApiResponse.Success()) { StatusCode = 200 };
    }

    public new OkObjectResult Ok(object value)
    {
        return new OkObjectResult(ApiResponse.Success(value)) { StatusCode = 200 };
    }

    public new NotFoundObjectResult NotFound()
    {
        return new NotFoundObjectResult(ApiResponse.Error()) { StatusCode = 404 };
    }

    public new NotFoundObjectResult NotFound(object value)
    {
        return new NotFoundObjectResult(ApiResponse.Error()) { StatusCode = 404 };
    }

    public new ConflictObjectResult Conflict()
    {
        return new ConflictObjectResult(ApiResponse.Error()) { StatusCode = 409 };
    }

    public new ConflictObjectResult Conflict(object value)
    {
        return new ConflictObjectResult(ApiResponse.Error()) { StatusCode = 409 };
    }

    public IActionResult Error(Result result)
    {
        var message = result.Error.Message;
        switch (result.Error.Type)
        {
            case ErrorType.Conflict:
                return message is null ? Conflict() : Conflict(message);
            case ErrorType.NotFound:
                return message is null ? NotFound() : NotFound(message);
            default:
                return message is null ? BadRequest() : BadRequest(message);
        }
    }
}
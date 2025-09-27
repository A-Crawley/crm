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
}
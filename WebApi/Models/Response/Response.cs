using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebApi.Models.Response;

public enum ResponseType
{
    Success,
    Error
}

public class ApiResponse
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResponseType ResponseType { get; internal set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Value { get; internal set; }

    public static ApiResponse Success(object value) => new() { Value = value, ResponseType = ResponseType.Success };
    public static ApiResponse Error() => new() { ResponseType = ResponseType.Error };
    public static ApiResponse Success() => new() { ResponseType = ResponseType.Success };
}

public class ApiResponse<T> : ApiResponse where T : class
{

}
using Domain.Enums;

namespace Domain.Models;

public class Error
{
    public ErrorType Type { get; }
    public string? Message { get; }
    private Error(ErrorType type, string? message)
    {
        Type = type;
        Message = message;
    }
    public static Error ConflictError(string? message = null) => new(ErrorType.Conflict, message);
    public static Error NotFoundError(string? message = null) => new(ErrorType.NotFound, message);
    public static Error GeneralError(string message) => new (ErrorType.Generic, message);
}
namespace Domain.Models;

public class Result
{
    private Error? _error;
    public Error Error => _error ?? throw new NullReferenceException();
    public bool IsSuccess => _error is null;

    protected Result(Error error)
    {
        _error = error;
    }

    protected Result(){}

    private static Result Success() => new();
    private static Result Failure(Error error) => new(error); 
}

public class Result<T> : Result where T : class
{
    private T? _value;
    
    public T Value => _value ?? throw new NullReferenceException();

    private Result(T value)
    {
        _value = value;
    }
    
    private Result(Error error): base(error) { }
    
    public static Result<T> Success(T value) => new (value);
    public static Result<T> Failure(Error error) => new(error);
    
    public static implicit operator T(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
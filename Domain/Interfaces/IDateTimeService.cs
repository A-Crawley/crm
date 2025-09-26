namespace Domain.Interfaces;

public interface IDateTimeService
{
    DateTimeOffset Now { get; }
}
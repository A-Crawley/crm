using Domain.Interfaces;

namespace Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
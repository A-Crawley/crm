using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Configuration
{
    public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IContext, Context>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<AuditableInterceptor>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IHashingService, HashingService>();
    }
}
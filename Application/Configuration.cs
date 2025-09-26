using Application.Queries.UserQueries;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Configuration
{
    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IGetUserByIdQuery, GetUserByIdQuery>();
    }
}
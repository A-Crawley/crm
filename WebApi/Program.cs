using Application;
using Domain.Settings;
using Infrastructure;
using Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddQueries();
builder.Services.AddCommands();
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddSingleton(() => builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>()
                                    ?? throw new Exception("AuthSettings not found")
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
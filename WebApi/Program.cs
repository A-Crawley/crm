using Application;
using Domain.Settings;
using Infrastructure;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSeq(builder.Configuration.GetSection("Seq"));
builder.Logging.Configure(options =>
{
    options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId;
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.Request |
                            HttpLoggingFields.RequestBody |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.Response |
                            HttpLoggingFields.ResponseBody;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddQueries();
builder.Services.AddCommands();
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddSingleton(() => builder.Configuration.GetSection("Jwt").Get<AuthSettings>()
                                    ?? throw new Exception("AuthSettings not found")
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();
app.UseHttpLogging();

app.Run();
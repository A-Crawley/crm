using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundWorkers;

public class AuditLogQueue
{
    private List<AuditLog> Logs { get; } = new();

    public void Add(AuditLog log)
    {
        Logs.Add(log);
    }

    public AuditLog? Dequeue()
    {
        var item = Logs.FirstOrDefault();
        if (item == null)
            return null;
        Logs.Remove(item);
        return item;
    }
}

public class AuditLogProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AuditLog> _logger;
    private readonly AuditLogQueue _auditLogQueue;

    public AuditLogProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<AuditLog> logger, AuditLogQueue auditLogQueue)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _auditLogQueue = auditLogQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting audit log queue processing...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var itemToProcess = _auditLogQueue.Dequeue();
            if (itemToProcess is null)
            {
                await Task.Delay(5000, stoppingToken);
                continue;
            }

            _logger.LogInformation("Processing audit log...");
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IContext>();
            if (context is null) throw new  ArgumentNullException(nameof(context));
            await context.AddAuditAsync(itemToProcess, stoppingToken);
        }
    }
}
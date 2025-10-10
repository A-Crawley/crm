using Infrastructure.Database;
using Infrastructure.Database.Entities;
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
    private readonly ILogger<AuditLog> _logger;
    private readonly AuditLogQueue _auditLogQueue;
    private readonly IContext _context;

    public AuditLogProcessor(ILogger<AuditLog> logger, AuditLogQueue auditLogQueue, IContext context)
    {
        _logger = logger;
        _auditLogQueue = auditLogQueue;
        _context = context;
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
            await _context.AddAuditAsync(itemToProcess, stoppingToken);
        }
    }
}
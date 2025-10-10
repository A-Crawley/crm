using System.Text;
using System.Text.Json;

namespace Infrastructure.Database.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public required string Table { get; set; }
    public int Reference { get; set; }
    public string? OldValue { get; set; }
    public string NewValue { get; set; }
    public int? Who { get; set; }
    public DateTimeOffset When { get; set; }

    public string GetHash()
    {
        var text = JsonSerializer.Serialize(this);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }
}
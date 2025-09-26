using Infrastructure.Database.Interfaces;

namespace Infrastructure.Database.Entities;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
    public int? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public int? ArchivedBy { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
}
using System.Security;

namespace Infrastructure.Database.Entities;

public interface IEntity
{
    int Id { get; set; }
    int? CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    int? UpdatedBy { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    int? ArchivedBy { get; set; }
    DateTimeOffset? ArchivedAt { get; set; }
}

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

public class User : Entity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
namespace Infrastructure.Database.Interfaces;

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
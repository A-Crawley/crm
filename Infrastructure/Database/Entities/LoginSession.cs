namespace Infrastructure.Database.Entities;

public class LoginSession : Entity
{
    public required int UserId { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTimeOffset Expiry { get; set; }

    public User User { get; set; } = null!;
}
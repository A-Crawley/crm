using System.Security;

namespace Infrastructure.Database.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    
    public ICollection<LoginSession>? LoginSessions { get; set; }
}
using System.Security;

namespace Infrastructure.Database.Entities;

public class User : Entity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
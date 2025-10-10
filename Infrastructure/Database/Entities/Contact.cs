namespace Infrastructure.Database.Entities;

public class Contact : Entity
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? LandlineNumber { get; set; }
}
using Domain.Structs;

namespace Domain.DTOs;

public record ContactDto
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public Email? Email { get; init; }
    public string? MobileNumber { get; init; }
    public string? LandlineNumber { get; init; }
    public required Auditable Created { get; init; }
    public Auditable? Updated { get; init; }
    public Auditable? Archived { get; init; }
}
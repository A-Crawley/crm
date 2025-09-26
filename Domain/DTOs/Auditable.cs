namespace Domain.DTOs;

public record Auditable(int? Id, DateTimeOffset At);
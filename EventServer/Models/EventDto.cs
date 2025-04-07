namespace EventServer.Models;

public record EventDto(
    string EventName,
    string Venue,
    string CreatedBy,
    DateTime StartAt,
    DateTime EndAt,
    List<string> Attenders,
    string? TenantId);
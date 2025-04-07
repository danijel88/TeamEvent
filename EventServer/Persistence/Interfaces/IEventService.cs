using EventServer.Models;

namespace EventServer.Persistence.Interfaces;

public interface IEventService
{
    Task<bool> SaveTeamEventAsync(EventDto request);
    Task<List<EventSummaryDto>> GetEventsByTenantIdAsync(string tenantId);
}
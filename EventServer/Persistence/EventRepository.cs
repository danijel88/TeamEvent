using EventServer.Domain;
using EventServer.Infrastructure;
using EventServer.Models;
using Microsoft.EntityFrameworkCore;
using TeamEvent.Server.Persistence.Interfaces;

namespace EventServer.Persistence;

public class EventRepository(ApplicationDbContext context) : IEventRepository
{
    private readonly ApplicationDbContext _context = context;

    /// <inheritdoc />
    public async Task<bool> AddEventAsync(EventDto request)
    {
        var teamEventSet = _context.Set<EventServer.Domain.TeamEvent>();


        var newEvent = new EventServer.Domain.TeamEvent(
            request.EventName,
            request.Venue,
            request.StartAt,
            request.EndAt,
            request.CreatedBy,
            request.TenantId
        );


        await teamEventSet.AddAsync(newEvent);
        await _context.SaveChangesAsync();

        await AddAttenders(request, newEvent);

        return true;
    }

    /// <inheritdoc />
    public async Task<List<EventSummaryDto>> GetEventsByTenantIdAsync(string tenantId)
    {
        var events = await _context
            .TeamEvents
            .Where(w => w.TenantId == tenantId)
            .Include(x => x.Attenders)
            .Select(s => new EventSummaryDto(s.Id,
                s.Name, s.Venue, s.CreatedBy, s.StartAt, s.EndAt,
                new List<string> { string.Join(", ", s.Attenders.Select(x => x.Email)) },
                s.TenantId))
            .ToListAsync();
        return events;
    }

    private async Task AddAttenders(EventDto request, Domain.TeamEvent newEvent)
    {
        var attendersSet = _context.Set<Attenders>();
        var attenders = request.Attenders
            .Select(a => new Attenders(a, newEvent.Id))
            .ToList();

        await attendersSet.AddRangeAsync(attenders);
        await _context.SaveChangesAsync();
    }
}
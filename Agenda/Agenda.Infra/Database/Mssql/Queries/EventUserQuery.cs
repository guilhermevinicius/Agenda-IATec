using Agenda.Domain.DTOs.EventUser;
using Agenda.Domain.Queries;
using Agenda.Infra.Database.Mssql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Database.Mssql.Queries;

public class EventUserQuery : IEventUserQuery
{
    private readonly AgendaMssqlDbContext _context;

    public EventUserQuery(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public async Task<EventUserDto?> ById(Guid eventUserId)
    {
        var eventUser = await _context.EventUser.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventUserId);
        return eventUser?.ToDomain();
    }

    public async Task<EventUserDto?> ByEventId(Guid eventId, Guid userId)
    {
        var eventUser = await _context.EventUser.AsNoTracking()
            .FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);
        return eventUser?.ToDomain();
    }
}
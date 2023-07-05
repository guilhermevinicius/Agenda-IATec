using Agenda.Domain.Entities;
using Agenda.Domain.Repositories;
using Agenda.Infra.Database.Mssql.Contexts;
using Agenda.Infra.Database.Mssql.Entities;

namespace Agenda.Infra.Database.Mssql.Repositories;

public class EventUserRepository : IEventUserRepository
{
    private readonly AgendaMssqlDbContext _context;

    public EventUserRepository(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public async Task Create(EventUser eventUser)
    {
        await _context.EventUser.AddAsync(new EventUserMssql(eventUser.Id, eventUser.UserId, eventUser.EventId,
            eventUser.IsAccepted));
        await _context.SaveChangesAsync();
    }

    public async Task Update(EventUser eventUser)
    {
        var entity = new EventUserMssql(eventUser.Id, eventUser.UserId, eventUser.EventId, eventUser.IsAccepted);
        _context.EventUser.Update(entity);
        await _context.SaveChangesAsync();
    }
}
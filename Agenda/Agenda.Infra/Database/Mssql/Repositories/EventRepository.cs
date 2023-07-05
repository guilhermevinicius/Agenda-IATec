using Agenda.Domain.Entities;
using Agenda.Domain.Repositories;
using Agenda.Infra.Database.Mssql.Contexts;
using Agenda.Infra.Database.Mssql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Database.Mssql.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AgendaMssqlDbContext _context;

    public EventRepository(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public async Task Create(Event e)
    {
        var eventMssql = new EventMssql(e.Id, e.Name.Value, e.Description, e.Date, e.Local, e.Type, e.UserId);
        await _context.Events.AddAsync(eventMssql);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Event e)
    {
        var eventMssql = new EventMssql(e.Id, e.Name.Value, e.Description, e.Date, e.Local, e.Type, e.UserId);
        _context.Events.Update(eventMssql);
        await _context.SaveChangesAsync();
    }

    public bool Remove(Guid eventId, Guid userId)
    {
        var even = _context.Events.AsNoTracking().FirstOrDefault(x => x.Id == eventId && x.UserId == userId);
        if (even == null)
            return false;

        _context.Remove(even);
        _context.SaveChanges();
        return true;
    }
}
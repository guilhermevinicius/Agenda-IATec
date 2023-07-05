using Agenda.Domain.DTOs.Event;
using Agenda.Domain.Enums;
using Agenda.Domain.Queries;
using Agenda.Infra.Database.Mssql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Database.Mssql.Queries;

public class EventQuery : IEventQuery
{
    private readonly AgendaMssqlDbContext _context;

    public EventQuery(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public async Task<EventDto?> ById(Guid eventId, Guid userId)
    {
        var events = await _context.Events.AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
        return events?.ToDomain();
    }

    public async Task<IList<EventDto>> AllByUserId(Guid userId)
    {
        return await _context.EventUser
            .Include(x => x.Event)
            .Where(x => x.UserId == userId && x.IsAccepted && x.Event.Date >= DateTime.Today)
            .Select(x => new EventDto
            {
                Id = x.Event.Id,
                UserId = x.Event.UserId,
                Active = x.Event.Active,
                Date = x.Event.Date,
                Type = x.Event.Type,
                Description = x.Event.Description,
                Local = x.Event.Local,
                Name = x.Event.Name,
                EventUserId = x.Id,
                IsOwner = x.Event.UserId == userId
            })
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Type)
            .ToListAsync();
    }

    public async Task<IList<EventDto>> AllOtherEvent(Guid userId)
    {
        return await _context.EventUser
            .Include(x => x.Event)
            .Where(x => x.UserId != userId && x.Event.UserId != userId && x.Event.Type == ETypeEvent.Shared &&
                        x.Event.Date >= DateTime.Today)
            .Select(x => new EventDto
            {
                Id = x.Event.Id,
                UserId = x.Event.UserId,
                Active = x.Event.Active,
                Date = x.Event.Date,
                Type = x.Event.Type,
                Description = x.Event.Description,
                Local = x.Event.Local,
                Name = x.Event.Name,
                EventUserId = x.Id,
                IsOwner = x.Event.UserId == userId
            })
            .OrderBy(x => x.Type)
            .ThenBy(x => x.Date)
            .ToListAsync();
    }

    public async Task<IList<EventDto>> AllNotAccepted(Guid userId)
    {
        return await _context.EventUser
            .Include(x => x.Event)
            .Where(x => x.UserId == userId && !x.IsAccepted && x.Event.Date >= DateTime.Today)
            .Select(x => new EventDto
            {
                Id = x.Event.Id,
                UserId = x.Event.UserId,
                Active = x.Event.Active,
                Date = x.Event.Date,
                Type = x.Event.Type,
                Description = x.Event.Description,
                Local = x.Event.Local,
                Name = x.Event.Name,
                EventUserId = x.Id,
                IsOwner = x.Event.UserId == userId
            })
            .OrderBy(x => x.Date)
            .ThenByDescending(x => x.Type)
            .ToListAsync();
    }

    public async Task<EventDto?> ByBetweenDate(DateTime date, Guid userId)
    {
        var events = await _context.Events
            .AsNoTracking()
            .Where(x => x.Date >= date && x.Date <= date)
            .FirstOrDefaultAsync(x => x.Active == true);
        return events?.ToDomain();
    }

    public async Task<IList<EventDto>> Search(string nameEvent, DateTime? date, Guid userId)
    {
        Console.WriteLine(date);
        return await _context.EventUser
            .Include(x => x.Event)
            .Where(x => x.UserId == userId && x.IsAccepted && (x.Event.Name.Contains(nameEvent) || x.Event.Date.Date == date) && x.Event.Date >= DateTime.Today)
            .Select(x => new EventDto
            {
                Id = x.Event.Id,
                UserId = x.Event.UserId,
                Active = x.Event.Active,
                Date = x.Event.Date,
                Type = x.Event.Type,
                Description = x.Event.Description,
                Local = x.Event.Local,
                Name = x.Event.Name,
                EventUserId = x.Id,
                IsOwner = x.Event.UserId == userId
            })
            .OrderBy(x => x.Date)
            .ThenByDescending(x => x.Type)
            .ToListAsync();
    }
}
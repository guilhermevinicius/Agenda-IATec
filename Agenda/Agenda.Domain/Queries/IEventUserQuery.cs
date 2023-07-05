using Agenda.Domain.DTOs.EventUser;

namespace Agenda.Domain.Queries;

public interface IEventUserQuery
{
    Task<EventUserDto?> ById(Guid eventUserId);
    Task<EventUserDto?> ByEventId(Guid eventId, Guid userId);
}
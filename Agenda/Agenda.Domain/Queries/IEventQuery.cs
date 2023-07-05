using Agenda.Domain.DTOs.Event;

namespace Agenda.Domain.Queries;

public interface IEventQuery
{
    Task<EventDto?> ById(Guid eventId, Guid userId);
    Task<IList<EventDto>> AllByUserId(Guid userId);
    Task<IList<EventDto>> AllOtherEvent(Guid userId);
    Task<IList<EventDto>> AllNotAccepted(Guid userId);
    Task<EventDto?> ByBetweenDate(DateTime date, Guid userId);
    Task<IList<EventDto>> Search(string nameEvent, DateTime? date, Guid userId);
}
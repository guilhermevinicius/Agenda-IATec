using Agenda.Shared.Entities;

namespace Agenda.Domain.Entities;

public class EventUser : Entity
{
    public EventUser(Guid userId, Guid eventId)
    {
        UserId = userId;
        EventId = eventId;
        IsAccepted = false;
    }
    
    public EventUser(Guid id, Guid userId, Guid eventId)
    {
        SetId(id);
        UserId = userId;
        EventId = eventId;
        IsAccepted = false;
    }

    public Guid UserId { get; }
    public Guid EventId { get; }
    public bool IsAccepted { get; private set; }

    public void AcceptedEvent()
    {
        IsAccepted = true;
    }
}
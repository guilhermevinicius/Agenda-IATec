using Agenda.Domain.DTOs.EventUser;

namespace Agenda.Infra.Database.Mssql.Entities;

public class EventUserMssql
{
    public EventUserMssql(Guid id, Guid userId, Guid eventId, bool isAccepted)
    {
        Id = id;
        UserId = userId;
        EventId = eventId;
        IsAccepted = isAccepted;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public bool IsAccepted { get; set; }
    public EventMssql Event { get; set; }
    public UserMssql User { get; set; }

    public EventUserDto? ToDomain()
    {
        return new EventUserDto
        {
            Id = Id,
            EventId = EventId,
            IsAccepted = IsAccepted,
            UserId = UserId
        };
    }
}
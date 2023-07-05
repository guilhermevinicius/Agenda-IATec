using Agenda.Domain.Enums;
using Agenda.Domain.ValueObjects;
using Agenda.Shared.Entities;

namespace Agenda.Domain.Entities;

public class Event : Entity
{
    public Event(Name name, string description, DateTime date, string local, ETypeEvent type, Guid userId)
    {
        Name = name;
        Description = description;
        Date = date;
        Local = local;
        Type = type;
        Active = true;
        UserId = userId;

        AddNotifications(name);
    }

    public Name Name { get; }
    public string Description { get; }
    public DateTime Date { get; }
    public string Local { get; }
    public ETypeEvent Type { get; }
    public bool Active { get; }
    public Guid UserId { get; }
}
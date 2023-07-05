using Agenda.Domain.DTOs.Event;
using Agenda.Domain.Enums;

namespace Agenda.Infra.Database.Mssql.Entities;

public class EventMssql
{
    public EventMssql(Guid id, string name, string description, DateTime date, string local, ETypeEvent type,
        Guid userId)
    {
        Id = id;
        Name = name;
        Description = description;
        Date = date;
        Local = local;
        Type = type;
        Active = true;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Local { get; set; }
    public ETypeEvent Type { get; set; }
    public Guid UserId { get; set; }
    public bool Active { get; set; }

    public EventDto? ToDomain()
    {
        return new EventDto
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Date = Date,
            Local = Local,
            Type = Type,
            UserId = UserId,
            Active = Active
        };
    }
}
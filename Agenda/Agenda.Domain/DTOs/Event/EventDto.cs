using Agenda.Domain.Enums;

namespace Agenda.Domain.DTOs.Event;

public class EventDto
{
    public Guid Id { get; set; }
    public Guid EventUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Local { get; set; }
    public ETypeEvent Type { get; set; }
    public Guid UserId { get; set; }
    public bool Active { get; set; }
    public bool IsOwner { get; set; }
}
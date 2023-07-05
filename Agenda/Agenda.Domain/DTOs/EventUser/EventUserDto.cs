namespace Agenda.Domain.DTOs.EventUser;

public class EventUserDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public bool IsAccepted { get; set; }
}
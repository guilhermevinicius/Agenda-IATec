using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.EventUser;

public class SharedEventUserCommand : Notifiable<Notification>, ICommand
{
    public string UserId { get; set; }
    public string EventId { get; set; }
    
    public void Validate()
    {
        AddNotifications(new Contract<SharedEventUserCommand>()
            .Requires()
            .IsNotNullOrEmpty(EventId, "EventId", "O campo é obrigatio")
            .IsNotNullOrEmpty(UserId, "UserId", "O campo é obrigatio")
        );
    }
}
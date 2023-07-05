using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.Event;

public class RemoveEventCommand : Notifiable<Notification>, ICommand
{
    public string EventId { get; set; }
    public string UserId { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<RemoveEventCommand>()
            .Requires()
            .IsNotNullOrEmpty(EventId, "EventId", "Campo obrigatorio")
            .IsNotNullOrEmpty(UserId, "UserId", "Campo obrigatorio")
        );
    }
}
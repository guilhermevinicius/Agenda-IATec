using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.EventUser;

public class CreateEventUserCommand : Notifiable<Notification>, ICommand
{
    public string EventId { get; set; }
    public string UserId { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateEventUserCommand>()
            .Requires()
            .IsNotNullOrEmpty(EventId, "EventId", "O campo é obrigatio")
            .IsNotNullOrEmpty(UserId, "UserId", "O campo é obrigatio")
        );
    }
}
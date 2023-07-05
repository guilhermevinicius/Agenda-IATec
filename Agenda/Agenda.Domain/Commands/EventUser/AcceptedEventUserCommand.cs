using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.EventUser;

public class AcceptedEventUserCommand : Notifiable<Notification>, ICommand
{
    public string UserId { get; set; }
    public string EventUserId { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<AcceptedEventUserCommand>()
            .Requires()
            .IsNotNullOrEmpty(EventUserId, "EventUserId", "O campo é obrigatio")
            .IsNotNullOrEmpty(UserId, "UserId", "O campo é obrigatio")
        );
    }
}
using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.Event;

public class UpdateEventCommand : Notifiable<Notification>, ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Local { get; set; }
    public bool IsShared { get; set; }
    public Guid UserId { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateEventCommand>()
            .Requires()
            .IsNotNullOrEmpty(Name, "Name", "Campo obrigatorio")
            .IsNotNullOrEmpty(Description, "Description", "Campo obrigatorio")
            .IsNotNullOrEmpty(Local, "Local", "Campo obrigatorio")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "O Campo deve contar pelo menos 3 caracteres")
            .IsGreaterOrEqualsThan(Description, 3, "Description", "O Campo deve contar pelo menos 3 caracteres")
        );
    }
}
using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.User;

public class CreateUserCommand : Notifiable<Notification>, ICommand
{
    public CreateUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; }
    public string Email { get; }
    public string Password { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateUserCommand>()
            .Requires()
            .IsNotNullOrEmpty(Name, "Name", "Campo obrigatorio")
            .IsNotNullOrEmpty(Password, "Password", "Campo obrigatorio")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "O Campo deve contar pelo menos 3 caracteres")
            .IsGreaterOrEqualsThan(Password, 5, "Password", "O Campo deve contar pelo menos 5 caracteres")
            .IsNotNullOrEmpty(Email, "E-mail", "Esse campo é obrigatorio")
            .IsEmail(Email, "E-mail", "Formato do e-mail invalido")
        );
    }
}
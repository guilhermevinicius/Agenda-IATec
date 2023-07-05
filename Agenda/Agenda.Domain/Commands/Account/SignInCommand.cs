using Agenda.Shared.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Agenda.Domain.Commands.Account;

public class SignInCommand : Notifiable<Notification>, ICommand
{
   public string Email { get; set; }
    public string Password { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<SignInCommand>()
            .Requires()
            .IsNotNullOrEmpty(Email, "E-mail", "Esse campo é obrigatorio")
            .IsNotNullOrEmpty(Password, "Senha", "Esse campo é obrigatorio")
            .IsEmail(Email, "E-mail", "Formato do e-mail invalido")
        );
    }
}
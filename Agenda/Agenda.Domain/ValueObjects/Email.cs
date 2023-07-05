using Agenda.Shared.ValueObjects;
using Flunt.Validations;

namespace Agenda.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string value)
    {
        Value = value;

        AddNotifications(new Contract<Email>()
            .Requires()
            .IsNotNullOrEmpty(Value, "E-mail", "Esse campo é obrigatorio")
            .IsEmail(Value, "E-mail", "Formato do e-mail invalido")
        );
    }

    public string Value { get; }
}
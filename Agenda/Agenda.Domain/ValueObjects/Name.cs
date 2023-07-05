using Agenda.Shared.ValueObjects;
using Flunt.Validations;

namespace Agenda.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string value)
    {
        Value = value;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(Value, "Name", "Campo obrigatorio")
            .IsGreaterOrEqualsThan(Value, 3, "Name", "O Campo deve contar pelo menos 3 caracteres"));
    }

    public string Value { get; }
}
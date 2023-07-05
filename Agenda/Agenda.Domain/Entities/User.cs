using Agenda.Domain.ValueObjects;
using Agenda.Shared.Entities;

namespace Agenda.Domain.Entities;

public class User : Entity
{
    public User(Name name, Email email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Active = true;

        AddNotifications(name, email);
    }

    public Name Name { get; }
    public Email Email { get; }
    public string Password { get; }
    public bool Active { get; private set; }

    public void Activated()
    {
        Active = true;
    }

    public void Deactivated()
    {
        Active = false;
    }
}
using Flunt.Notifications;

namespace Agenda.Shared.Entities;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
    
}
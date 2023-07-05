using Agenda.Domain.Entities;

namespace Agenda.Domain.Repositories;

public interface IEventRepository
{
   Task Create(Event e);
   Task Update(Event e);
   bool Remove(Guid eventId, Guid userId);
}
using Agenda.Domain.Entities;

namespace Agenda.Domain.Repositories;

public interface IEventUserRepository
{
    Task Create(EventUser eventUser);
    Task Update(EventUser eventUser);
}
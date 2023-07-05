using Agenda.Domain.Entities;

namespace Agenda.Domain.Repositories;

public interface IUserRepository
{
    void Create(User user);
}
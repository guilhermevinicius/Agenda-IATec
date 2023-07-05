using Agenda.Domain.Entities;
using Agenda.Domain.Repositories;
using Agenda.Infra.Database.Mssql.Contexts;
using Agenda.Infra.Database.Mssql.Entities;

namespace Agenda.Infra.Database.Mssql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AgendaMssqlDbContext _context;

    public UserRepository(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public void Create(User user)
    {
        var userMssql = new UserMssql(user.Id, user.Name.Value, user.Email.Value, user.Active);
        userMssql.Password = user.Password;
        _context.Users.Add(userMssql);
        _context.SaveChanges();
    }
}
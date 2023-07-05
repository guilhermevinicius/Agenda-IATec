using Agenda.Domain.DTOs.User;
using Agenda.Domain.Queries;
using Agenda.Infra.Database.Mssql.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Database.Mssql.Queries;

public class UserQuery : IUserQuery
{
    private readonly AgendaMssqlDbContext _context;

    public UserQuery(AgendaMssqlDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> ById(Guid userId)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        return user?.ToDomain();
    }

    public async Task<UserDto?> ByEmail(string email)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        return user?.ToDomain();
    }

    public async Task<UserWithPasswordDto?> ByEmailWithPassword(string email)
    {
        return await _context.Users.AsNoTracking()
            .Select(x => new UserWithPasswordDto
            {
                Id = x.Id,
                Email = x.Email,
                Active = x.Active,
                Name = x.Name,
                Password = x.Password
            }).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IList<UserDto>> All(Guid userId)
    {
        IList<UserDto> usersDtos = new List<UserDto>();
        var users = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id != userId)
            .ToListAsync();

        foreach (var user in users)
            if (user != null)
                usersDtos.Add(user.ToDomain());

        return usersDtos;
    }
}
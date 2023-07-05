using Agenda.Domain.DTOs.User;

namespace Agenda.Domain.Queries;

public interface IUserQuery
{
    Task<UserDto?> ById(Guid userId);
    Task<UserDto?> ByEmail(string email);
    Task<UserWithPasswordDto?> ByEmailWithPassword(string email);
    Task<IList<UserDto>> All(Guid userId);
}
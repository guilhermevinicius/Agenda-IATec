using Agenda.Domain.DTOs.User;

namespace Agenda.Infra.Database.Mssql.Entities;

public class UserMssql
{
    public UserMssql(Guid id, string name, string email, bool active)
    {
        Id = id;
        Name = name;
        Email = email;
        Active = active;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public bool Active { get; set; }

    public UserDto? ToDomain()
    {
        return new UserDto
        {
            Id = Id,
            Name = Name,
            Email = Email,
            Active = Active
        };
    }
}
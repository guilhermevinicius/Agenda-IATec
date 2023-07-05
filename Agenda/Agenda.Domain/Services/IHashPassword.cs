namespace Agenda.Domain.Services;

public interface IHashPassword
{
    string Generate(string password);
    bool Verify(string passwordHash, string inputPassword);
}
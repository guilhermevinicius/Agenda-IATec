namespace Agenda.Domain.Services;

public interface IGenerateJwt
{
    string Generate(Guid userId);
}
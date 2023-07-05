namespace Agenda.Shared.Commands.Contracts;

public interface ICommandResult
{
    public bool Success { get; }
    public string? Error { get; }
    public dynamic? Body { get; }
}
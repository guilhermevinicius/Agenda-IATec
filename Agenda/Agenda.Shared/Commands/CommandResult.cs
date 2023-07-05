using Agenda.Shared.Commands.Contracts;

namespace Agenda.Shared.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult(bool success, string error, dynamic body)
    {
        Success = success;
        Error = error;
        Body = body;
    }

    public CommandResult(bool success, string? error)
    {
        Success = success;
        Error = error;
        Body = null;
    }

    public CommandResult(bool success, dynamic body)
    {
        Success = success;
        Error = null;
        Body = body;
    }

    public bool Success { get; }
    public string? Error { get; }
    public dynamic? Body { get; }
}
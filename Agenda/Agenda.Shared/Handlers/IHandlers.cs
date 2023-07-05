using Agenda.Shared.Commands.Contracts;

namespace Agenda.Shared.Handlers;

public interface IHandlers<T> where T : ICommand
{
    Task<ICommandResult> Handler(T command);
}
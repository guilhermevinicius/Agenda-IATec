using Agenda.Domain.Commands.Event;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.Event;

public class RemoveEventHandler : IHandlers<RemoveEventCommand>
{
    private readonly IEventQuery _eventQuery;
    private readonly IEventRepository _eventRepository;

    public RemoveEventHandler(IEventQuery eventQuery, IEventRepository eventRepository)
    {
        _eventQuery = eventQuery;
        _eventRepository = eventRepository;
    }

    public async Task<ICommandResult> Handler(RemoveEventCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var eventById = await _eventQuery.ById(Guid.Parse(command.EventId), Guid.Parse(command.UserId));
        if (eventById == null)
            return new CommandResult(false, "Event not found");

        var isRemove = _eventRepository.Remove(Guid.Parse(command.EventId), Guid.Parse(command.UserId));
        if (!isRemove)
            return new CommandResult(false, "Error on remove this event");

        return new CommandResult(true, null);
    }
}
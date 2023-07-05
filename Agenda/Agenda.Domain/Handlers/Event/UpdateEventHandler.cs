using Agenda.Domain.Commands.Event;
using Agenda.Domain.Enums;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Domain.ValueObjects;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.Event;

public class UpdateEventHandler : IHandlers<UpdateEventCommand>
{
    private readonly IEventQuery _eventQuery;
    private readonly IEventRepository _eventRepository;

    public UpdateEventHandler(IEventRepository eventRepository, IEventQuery eventQuery)
    {
        _eventRepository = eventRepository;
        _eventQuery = eventQuery;
    }

    public async Task<ICommandResult> Handler(UpdateEventCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var eventById = await _eventQuery.ById(command.Id, command.UserId);
        if (eventById == null)
            return new CommandResult(false, "Event not found");
        
        var events = new Entities.Event(
            new Name(command.Name),
            command.Description,
            command.Date,
            command.Local,
            command.IsShared ? ETypeEvent.Shared : ETypeEvent.Exclusive,
            command.UserId
        );
        events.SetId(eventById.Id);

        await _eventRepository.Update(events);

        return new CommandResult(true, null);
    }
}
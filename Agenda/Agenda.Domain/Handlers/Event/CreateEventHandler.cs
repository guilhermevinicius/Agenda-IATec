using Agenda.Domain.Commands.Event;
using Agenda.Domain.Enums;
using Agenda.Domain.Repositories;
using Agenda.Domain.ValueObjects;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.Event;

public class CreateEventHandler : IHandlers<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventUserRepository _eventUserRepository;

    public CreateEventHandler(IEventRepository eventRepository, IEventUserRepository eventUserRepository)
    {
        _eventRepository = eventRepository;
        _eventUserRepository = eventUserRepository;
    }

    public async Task<ICommandResult> Handler(CreateEventCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var events = new Entities.Event(
            new Name(command.Name),
            command.Description,
            command.Date,
            command.Local,
            command.IsShared ? ETypeEvent.Shared : ETypeEvent.Exclusive,
            command.UserId
        );

        var eventUser = new Entities.EventUser(events.UserId, events.Id);
        eventUser.AcceptedEvent();

        await _eventRepository.Create(events);
        await _eventUserRepository.Create(eventUser);

        return new CommandResult(true, null);
    }
}
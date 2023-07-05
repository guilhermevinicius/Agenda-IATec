using Agenda.Domain.Commands.EventUser;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.EventUser;

public class CreateEventUserHandler : IHandlers<CreateEventUserCommand>
{
    private readonly IEventUserQuery _eventUserQuery;
    private readonly IEventUserRepository _eventUserRepository;

    public CreateEventUserHandler(IEventUserRepository eventUserRepository, IEventUserQuery eventUserQuery)
    {
        _eventUserRepository = eventUserRepository;
        _eventUserQuery = eventUserQuery;
    }

    public async Task<ICommandResult> Handler(CreateEventUserCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var eventUserByEventId = await _eventUserQuery.ByEventId(Guid.Parse(command.EventId), Guid.Parse(command.UserId));
        if (eventUserByEventId != null)
            return new CommandResult(false, "Você já tem esse evento");
        
        var eventUser = new Entities.EventUser(
            Guid.Parse(command.UserId),
            Guid.Parse(command.EventId)
        );
        eventUser.AcceptedEvent();

        await _eventUserRepository.Create(eventUser);

        return new CommandResult(true, null);
    }
}
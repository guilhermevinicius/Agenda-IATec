using Agenda.Domain.Commands.EventUser;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.EventUser;

public class AcceptedEventUserHandler : IHandlers<AcceptedEventUserCommand>
{
    private readonly IEventUserQuery _eventUserQuery;
    private readonly IEventUserRepository _eventUserRepository;

    public AcceptedEventUserHandler(IEventUserQuery eventUserQuery, IEventUserRepository eventUserRepository)
    {
        _eventUserQuery = eventUserQuery;
        _eventUserRepository = eventUserRepository;
    }

    public async Task<ICommandResult> Handler(AcceptedEventUserCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var eventUserById = await _eventUserQuery.ById(Guid.Parse(command.EventUserId));
        if(eventUserById == null)
            return new CommandResult(false, "Event not found");

        var eventUser = new Entities.EventUser(eventUserById.Id, eventUserById.UserId, eventUserById.EventId);
        eventUser.AcceptedEvent();

        await _eventUserRepository.Update(eventUser);
        
        return new CommandResult(true, null);
    }
}
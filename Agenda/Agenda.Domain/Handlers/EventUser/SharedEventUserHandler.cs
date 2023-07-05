using Agenda.Domain.Commands.EventUser;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.EventUser;

public class SharedEventUserHandler : IHandlers<SharedEventUserCommand>
{
    private readonly IEventQuery _eventQuery;
    private readonly IEventUserRepository _eventUserRepository;
    private readonly IUserQuery _userQuery;

    public SharedEventUserHandler(IEventQuery eventQuery, IUserQuery userQuery,
        IEventUserRepository eventUserRepository)
    {
        _eventQuery = eventQuery;
        _userQuery = userQuery;
        _eventUserRepository = eventUserRepository;
    }

    public async Task<ICommandResult> Handler(SharedEventUserCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var eventById = await _eventQuery.ById(Guid.Parse(command.EventId), Guid.Parse(command.UserId));
        if (eventById == null)
            return new CommandResult(false, "Event not found");

        var userById = await _userQuery.ById(Guid.Parse(command.UserId));
        if (userById == null)
            return new CommandResult(false, "User not found");

        var userEvent = new Entities.EventUser(userById.Id, eventById.Id);
        await _eventUserRepository.Create(userEvent);

        return new CommandResult(true, null);
    }
}
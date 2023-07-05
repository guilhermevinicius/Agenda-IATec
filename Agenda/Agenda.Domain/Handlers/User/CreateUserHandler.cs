using Agenda.Domain.Commands.User;
using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Domain.Services;
using Agenda.Domain.ValueObjects;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.User;

public class CreateUserHandler : IHandlers<CreateUserCommand>
{
    private readonly IHashPassword _hashPassword;
    private readonly IUserQuery _userQuery;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserQuery userQuery, IUserRepository userRepository, IHashPassword hashPassword)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _hashPassword = hashPassword;
    }

    public async Task<ICommandResult> Handler(CreateUserCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var userByEmail = await _userQuery.ByEmail(command.Email);
        if (userByEmail != null)
            return new CommandResult(false, "This email is already being used");

        var hashPassword = _hashPassword.Generate(command.Password);

        var user = new Entities.User(
            new Name(command.Name),
            new Email(command.Email),
            hashPassword
        );

        _userRepository.Create(user);

        return new CommandResult(true, null);
    }
}
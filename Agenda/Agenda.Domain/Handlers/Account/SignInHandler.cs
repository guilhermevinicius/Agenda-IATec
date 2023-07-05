using Agenda.Domain.Commands.Account;
using Agenda.Domain.Queries;
using Agenda.Domain.Services;
using Agenda.Shared.Commands;
using Agenda.Shared.Commands.Contracts;
using Agenda.Shared.Handlers;

namespace Agenda.Domain.Handlers.Account;

public class SignInHandler : IHandlers<SignInCommand>
{
    private readonly IGenerateJwt _generateJwt;
    private readonly IUserQuery _userQuery;
    private readonly IHashPassword _hashPassword;

    public SignInHandler(IUserQuery userQuery, IGenerateJwt generateJwt, IHashPassword hashPassword)
    {
        _userQuery = userQuery;
        _generateJwt = generateJwt;
        _hashPassword = hashPassword;
    }

    public async Task<ICommandResult> Handler(SignInCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new CommandResult(false, command.Notifications);

        var user = await _userQuery.ByEmailWithPassword(command.Email);
        if (user == null)
            return new CommandResult(false, "E-mail invalido");

        var isValid = _hashPassword.Verify(user.Password, command.Password);
        if (!isValid)
            return new CommandResult(false, "E-mail/Senha invalids");
        
        var accessToken = _generateJwt.Generate(user.Id);

        return new CommandResult(true, new { accessToken });
    }
}
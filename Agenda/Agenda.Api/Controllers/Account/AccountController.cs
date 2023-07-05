using Agenda.Api.Helpers;
using Agenda.Domain.Commands.Account;
using Agenda.Domain.Commands.User;
using Agenda.Domain.Handlers.Account;
using Agenda.Domain.Handlers.User;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers.Account;

[ApiController]
[Route("v1")]
public class AccountController : ControllerBase
{
    [HttpPost("signin")]
    public async Task<IResult> SignIn([FromBody] SignInCommand command, [FromServices] SignInHandler handler)
    {
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok(result.Body);
    }

    [HttpPost("signup")]
    public async Task<IResult> SignUp([FromBody] CreateUserCommand command, [FromServices] CreateUserHandler handler)
    {
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok(result.Body);
    }
}
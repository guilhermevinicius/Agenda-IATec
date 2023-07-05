using System.Security.Claims;
using Agenda.Api.Helpers;
using Agenda.Domain.Commands.EventUser;
using Agenda.Domain.Handlers.EventUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers.EventUser;

[ApiController]
[Route("v1")]
[Authorize]
public class EventUserController : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirstValue("uid")!);
    
    [HttpPost("event/user")]
    public async Task<IResult> CreateEventUser([FromBody] CreateEventUserCommand command, [FromServices] CreateEventUserHandler handler)
    {
        command.UserId = UserId.ToString();
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok("ok");
    }

    [HttpPost("event/shared")]
    public async Task<IResult> SharedEventUser([FromBody] SharedEventUserCommand command, [FromServices] SharedEventUserHandler handler)
    {
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok("ok");
    }
    
    [HttpPut("event/accepted/{eventUserId}")]
    public async Task<IResult> AcceptedEvent([FromRoute] string eventUserId, [FromServices] AcceptedEventUserHandler handler)
    {
        var command = new AcceptedEventUserCommand
        {
            UserId = UserId.ToString(),
            EventUserId = eventUserId
        };
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok("ok");   
    }
}
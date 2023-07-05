using System.Security.Claims;
using Agenda.Api.Helpers;
using Agenda.Domain.Commands.Event;
using Agenda.Domain.Handlers.Event;
using Agenda.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers.Event;

[ApiController]
[Route("v1")]
[Authorize]
public class EventController : ControllerBase
{
    private readonly IEventQuery _eventQuery;

    public EventController(IEventQuery eventQuery)
    {
        _eventQuery = eventQuery;
    }

    public Guid UserId => Guid.Parse(User.FindFirstValue("uid")!);

    [HttpGet("event/{eventId:guid}")]
    public async Task<IResult> ById([FromRoute] Guid eventId)
    {
        var e = await _eventQuery.ById(eventId, UserId);
        if(e == null)
            return HttpHelper.NoContent();

        return HttpHelper.Ok(e);
    }
    
    [HttpGet("events")]
    public async Task<IResult> All() => HttpHelper.Ok( await _eventQuery.AllByUserId(UserId));

    [HttpGet("events/others")]
    public async Task<IResult> AllShared() => HttpHelper.Ok(await _eventQuery.AllOtherEvent(UserId));

    [HttpGet("events/not/accepted")]
    public async Task<IResult> AllEventsNotAccepted() => HttpHelper.Ok(await _eventQuery.AllNotAccepted(UserId));

    [HttpGet("event/search")]
    public async Task<IResult> SearchEvent([FromQuery]string q, [FromQuery]DateTime? date)
    {
        return HttpHelper.Ok(await _eventQuery.Search(q, date, UserId));
    }

    [HttpPost("event")]
    public async Task<IResult> Create(
        [FromBody] CreateEventCommand command,
        [FromServices] CreateEventHandler handler
    )
    {
        command.UserId = UserId;
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok("evento criado com sucesso");
    }
    
    [HttpPut("event/{id}")]
    public async Task<IResult> Update(
        [FromRoute] string id,
        [FromBody] UpdateEventCommand command,
        [FromServices] UpdateEventHandler handler
    )
    {
        command.Id = Guid.Parse(id);
        command.UserId = UserId;
        command.Validate();
        if (!command.IsValid)
            return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

        var result = await handler.Handler(command);
        if (!result.Success)
            return HttpHelper.UnprocessableEntity(result.Error);

        return HttpHelper.Ok("evento atualizado com sucesso");
    }

    [HttpDelete("event/{eventId:guid}")]
    public async Task<IResult> Remove([FromRoute] Guid eventId, [FromServices] RemoveEventHandler handler)
    {
        try
        {
            var command = new RemoveEventCommand
            {
                EventId = eventId.ToString(),
                UserId = UserId.ToString()
            };
            command.Validate();
            if (!command.IsValid)
                return HttpHelper.BadRequest(HttpHelper.ConvertNotificationToString(command.Notifications));

            var result = await handler.Handler(command);
            if (!result.Success)
                return HttpHelper.UnprocessableEntity(result.Error);

            return HttpHelper.Ok("evento removido com sucesso");
        }
        catch
        {
            return HttpHelper.ServerError();
        }
    }
}
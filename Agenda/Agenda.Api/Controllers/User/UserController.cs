using System.Security.Claims;
using Agenda.Api.Helpers;
using Agenda.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers.User;

[ApiController]
[Route("v1")]
[Authorize]
public class UserController : ControllerBase
{
    Guid UserId => Guid.Parse(User.FindFirstValue("uid")!);
    private readonly IUserQuery _userQuery;

    public UserController(IUserQuery userQuery) => _userQuery = userQuery;

    [HttpGet("users")]
    public async Task<IResult> All()
    {
        var users = await _userQuery.All(UserId);
        if (users.Count == 0)
            return HttpHelper.NoContent();

        return HttpHelper.Ok(users);
    }
}
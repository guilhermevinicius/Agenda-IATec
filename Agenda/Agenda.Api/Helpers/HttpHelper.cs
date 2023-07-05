using Agenda.Shared.Commands;
using Flunt.Notifications;

namespace Agenda.Api.Helpers;

public static class HttpHelper
{
    public static IResult Ok(dynamic? body)
    {
        return Results.Ok(new CommandResult(true, body));
    }

    public static IResult BadRequest(string message)
    {
        return Results.BadRequest(new CommandResult(false, message));
    }

    public static IResult UnprocessableEntity(string? message)
    {
        return Results.UnprocessableEntity(new CommandResult(false, message));
    }

    public static IResult NoContent()
    {
        return Results.NoContent();
    }

    public static string ConvertNotificationToString(IReadOnlyCollection<Notification> notifications)
    {
        var notification = notifications.First();
        return $"{notification.Key}: {notification.Message}";
    }

    public static IResult ServerError()
    {
        return Results.StatusCode(500);
    }
}
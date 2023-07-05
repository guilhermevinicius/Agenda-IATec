using Agenda.Domain.Handlers.Account;
using Agenda.Domain.Handlers.Event;
using Agenda.Domain.Handlers.EventUser;
using Agenda.Domain.Handlers.User;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infra.IoC;

public static class HandlersIoC
{
    public static void Handlers(this IServiceCollection services)
    {
        services.AddTransient<CreateUserHandler>();
        services.AddTransient<SignInHandler>();
        services.AddTransient<CreateEventHandler>();
        services.AddTransient<RemoveEventHandler>();
        services.AddTransient<CreateEventUserHandler>();
        services.AddTransient<AcceptedEventUserHandler>();
        services.AddTransient<SharedEventUserHandler>();
        services.AddTransient<UpdateEventHandler>();

    }
}
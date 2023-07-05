using Agenda.Domain.Queries;
using Agenda.Domain.Repositories;
using Agenda.Infra.Database.Mssql.Queries;
using Agenda.Infra.Database.Mssql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Infra.IoC;

public static class RepositoriesIoC
{
    public static void Repositories(this IServiceCollection Services)
    {
        Services.AddTransient<IUserRepository, UserRepository>();
        Services.AddTransient<IEventRepository, EventRepository>();
        Services.AddTransient<IEventUserRepository, EventUserRepository>();
        Services.AddTransient<IUserQuery, UserQuery>();
        Services.AddTransient<IEventQuery, EventQuery>();
        Services.AddTransient<IEventUserQuery, EventUserQuery>();
    }
}
using Agenda.Infra.Database.Mssql.Entities;
using Agenda.Infra.Database.Mssql.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Database.Mssql.Contexts;

public class AgendaMssqlDbContext : DbContext
{
    public AgendaMssqlDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserMssql> Users { get; set; }
    public DbSet<EventMssql> Events { get; set; }
    public DbSet<EventUserMssql> EventUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMssqlMapping());
        modelBuilder.ApplyConfiguration(new EventMssqlMapping());
        modelBuilder.ApplyConfiguration(new EventUserMapping());
    }
}
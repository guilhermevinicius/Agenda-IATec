using Agenda.Infra.Database.Mssql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infra.Database.Mssql.Mappings;

public class EventMssqlMapping : IEntityTypeConfiguration<EventMssql>
{
    public void Configure(EntityTypeBuilder<EventMssql> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnType("DATETIME");

        builder.Property(x => x.Local)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Active)
            .HasColumnType("BIT")
            .HasDefaultValue(true);

        builder.HasIndex(x => x.UserId);
    }
}
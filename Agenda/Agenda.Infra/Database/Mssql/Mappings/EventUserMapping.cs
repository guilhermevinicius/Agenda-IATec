using Agenda.Infra.Database.Mssql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infra.Database.Mssql.Mappings;

public class EventUserMapping : IEntityTypeConfiguration<EventUserMssql>
{
    public void Configure(EntityTypeBuilder<EventUserMssql> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.EventId)
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.IsAccepted)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.HasOne(x => x.User);
        builder.HasOne(x => x.Event);
    }
}
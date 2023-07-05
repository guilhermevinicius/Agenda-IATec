using Agenda.Infra.Database.Mssql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infra.Database.Mssql.Mappings;

public class UserMssqlMapping : IEntityTypeConfiguration<UserMssql>
{
    public void Configure(EntityTypeBuilder<UserMssql> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");

        builder.Property(p => p.Password)
            .IsRequired()
            .HasColumnType("VARCHAR(255)");
        
        builder.Property(x => x.Active)
            .HasColumnType("BIT")
            .HasDefaultValue(true);
    }
}
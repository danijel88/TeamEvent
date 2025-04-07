using EventServer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventServer.Infrastructure.Config;

public class AttendersConfig : IEntityTypeConfiguration<Attenders>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Attenders> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Email)
            .IsRequired()
            .HasDefaultValue(DbConstSchema.DEFAULT_EMAIL_LENGTH);
        builder.Property(p => p.TeamEventId)
            .IsRequired();
    }
}
namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class TournamentFormatConfiguration : IEntityTypeConfiguration<TournamentFormat>
    {
        public void Configure(EntityTypeBuilder<TournamentFormat> builder)
        {

            builder.HasIndex(tf => new { tf.Name, tf.GameId })
                .IsUnique();

            builder.Property(tf => tf.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            builder.Property(tf => tf.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            builder.HasMany(tf => tf.Teams)
                .WithOne(t => t.TournamentFormat)
                .HasForeignKey(t => t.TournamentFormatId);
        }
    }
}

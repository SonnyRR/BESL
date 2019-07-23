namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class TournamentConfigration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasOne(c => c.Format)
                .WithMany(g => g.Tournaments)
                .HasForeignKey(c => c.FormatId);

            builder.HasMany(c => c.Tables)
                .WithOne(ct => ct.Tournament)
                .HasForeignKey(ct => ct.TournamentId);

            builder.Property(c => c.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);
        }
    }
}

namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(g => g.CompetitionTypes)
                .WithOne(ct => ct.Game)
                .HasForeignKey(ct => ct.GameId);

            builder.Property(g => g.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(40);

            builder.Property(g => g.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);
        }
    }
}

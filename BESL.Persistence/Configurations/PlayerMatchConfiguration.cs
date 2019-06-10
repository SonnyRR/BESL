namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class PlayerMatchConfiguration : IEntityTypeConfiguration<PlayerMatch>
    {
        public void Configure(EntityTypeBuilder<PlayerMatch> builder)
        {
            builder.HasKey(pm => new { pm.PlayerId, pm.MatchId });

            builder.HasOne(pm => pm.Player)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(pm => pm.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Match)
                .WithMany(m => m.ParticipatedPlayers)
                .HasForeignKey(pm => pm.MatchId);
        }
    }
}

using BESL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BESL.Persistence.Configurations
{
    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired();

            builder.HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired();

            builder.HasOne(m => m.Winner)
                .WithMany(t => t.WonMatches)
                .HasForeignKey(m => m.WinnerTeamId);

        }
    }
}

using BESL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BESL.Persistence.Configurations
{
    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(m => m.CompetitionTableResult)
                .WithMany(ttr => ttr.PlayedMatches)
                .HasForeignKey(m => m.CompetitionTableResultId);
        }
    }
}

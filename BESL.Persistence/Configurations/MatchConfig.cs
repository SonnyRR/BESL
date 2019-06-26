namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(m => m.TeamTableResult)
                .WithMany(ttr => ttr.PlayedMatches)
                .HasForeignKey(m => m.TeamTableResultId);
        }
    }
}

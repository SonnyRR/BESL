namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using BESL.Domain.Entities;

    public class TeamTableResultConfiguration : IEntityTypeConfiguration<TeamTableResult>
    {
        public void Configure(EntityTypeBuilder<TeamTableResult> builder)
        {
            builder.HasIndex(ttr => new { ttr.TeamId, ttr.TournamentTableId })
                .IsUnique();
        }
    }
}

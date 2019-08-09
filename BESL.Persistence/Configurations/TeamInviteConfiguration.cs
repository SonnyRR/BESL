namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using BESL.Domain.Entities;

    public class TeamInviteConfiguration : IEntityTypeConfiguration<TeamInvite>
    {
        public void Configure(EntityTypeBuilder<TeamInvite> builder)
        {
            builder.HasIndex(x => new { x.TeamId, x.PlayerId })
                .IsUnique();
        }
    }
}

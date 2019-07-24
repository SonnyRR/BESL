namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Owner)
                .WithMany(p => p.OwnedTeams)
                .HasForeignKey(t => t.OwnerId);

            //builder.HasOne(t => t.CurrentActiveTeamTableResult)
            //    .WithOne();

            builder.HasMany(t => t.PreviousTeamTableResults)
                .WithOne(tr => tr.Team)
                .HasForeignKey(tr => tr.TeamId);

            builder.Property(t => t.Name)
                .HasMaxLength(35)
                .IsUnicode()
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(1000)
                .IsUnicode();

            builder.Property(t => t.HomepageUrl)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}


namespace BESL.Persistence.Configurations
{
    using BESL.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Owner)
                .WithMany(p => p.OwnedTeams)
                .HasForeignKey(t => t.OwnerId);

            builder.HasOne(t => t.Game)
                .WithMany(g => g.Teams)
                .HasForeignKey(t => t.GameId);

            builder.Property(t => t.Name)
                .HasMaxLength(25)
                .IsUnicode()
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(10000)
                .IsUnicode();

            builder.Property(t => t.HomepageUrl)
                .HasMaxLength(256)
                .IsRequired(false);
        }
    }
}

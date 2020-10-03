namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Entities;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasMany(p => p.OwnedTeams)
                .WithOne(t => t.Owner)
                .HasForeignKey(t => t.OwnerId)
                .IsRequired();

            builder.HasMany(p => p.Invites)
                .WithOne(i => i.Player)
                .HasForeignKey(i => i.PlayerId)
                .IsRequired();
        }
    }
}

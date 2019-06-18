namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class CompetitionConfigration : IEntityTypeConfiguration<Competition>
    {
        public void Configure(EntityTypeBuilder<Competition> builder)
        {
            builder.HasOne(c => c.Game)
                .WithMany(g => g.Competitions)
                .HasForeignKey(c => c.GameId);

            builder.Property(c => c.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(35);

            builder.Property(c => c.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);
        }
    }
}

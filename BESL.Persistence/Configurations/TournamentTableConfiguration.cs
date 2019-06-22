namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class CompetitionTableConfiguration : IEntityTypeConfiguration<TournamentTable>
    {
        public void Configure(EntityTypeBuilder<TournamentTable> builder)
        {
            builder.Property(ct => ct.Name)
                .HasMaxLength(15)
                .IsUnicode()
                .IsRequired();

            builder.HasOne(ct => ct.Tournament)
                .WithMany(c => c.Tables)
                .HasForeignKey(ct => ct.TournamentId);

            builder.HasMany(ct => ct.SignedUpTeams)
                .WithOne(t => t.CurrentActiveTournamentTable)
                .HasForeignKey(t => t.CurrentActiveTournamentTableId);

            builder.HasMany(ct => ct.TeamTableResults)
                .WithOne(tr => tr.TournamentTable)
                .HasForeignKey(tr => tr.TournamentTableId);
        }
    }
}

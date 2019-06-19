namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class CompetitionTableConfiguration : IEntityTypeConfiguration<CompetitionTable>
    {
        public void Configure(EntityTypeBuilder<CompetitionTable> builder)
        {
            builder.Property(ct => ct.Name)
                .HasMaxLength(15)
                .IsUnicode()
                .IsRequired();

            builder.HasOne(ct => ct.Competition)
                .WithMany(c => c.Tables)
                .HasForeignKey(ct => ct.CompetitionId);

            builder.HasMany(ct => ct.SignedUpTeams)
                .WithOne(t => t.CurrentActiveCompetitionTable)
                .HasForeignKey(t => t.CurrentActiveCompetitionTableId);

            builder.HasMany(ct => ct.CompetitionTableResults)
                .WithOne(tr => tr.CompetitionTable)
                .HasForeignKey(tr => tr.CompetitionTableId);
        }
    }
}

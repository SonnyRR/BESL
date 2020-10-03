namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Entities;

    public class TournamentTableConfiguration : IEntityTypeConfiguration<TournamentTable>
    {
        public void Configure(EntityTypeBuilder<TournamentTable> builder)
        {
            builder.Property(tt => tt.Name)
                .HasMaxLength(15)
                .IsUnicode()
                .IsRequired();

            builder.HasOne(tt => tt.Tournament)
                .WithMany(c => c.Tables)
                .HasForeignKey(ct => ct.TournamentId);

            builder.HasMany(tt => tt.TeamTableResults)
                .WithOne(tr => tr.TournamentTable)
                .HasForeignKey(tr => tr.TournamentTableId);

            builder.HasMany(tt => tt.PlayWeeks)
                .WithOne(pw => pw.TournamentTable)
                .HasForeignKey(pw => pw.TournamentTableId);
        }
    }
}

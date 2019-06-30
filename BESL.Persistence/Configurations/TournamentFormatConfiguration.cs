﻿namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using BESL.Domain.Entities;

    public class TournamentFormatConfiguration : IEntityTypeConfiguration<TournamentFormat>
    {
        public void Configure(EntityTypeBuilder<TournamentFormat> builder)
        {
            builder.Property(tf => tf.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(25);

            builder.Property(tf => tf.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);
        }
    }
}

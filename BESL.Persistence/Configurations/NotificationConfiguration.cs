﻿namespace BESL.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using BESL.Entities;

    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.Player)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.PlayerId);

            builder.Property(n => n.Content)
                .IsUnicode()
                .HasMaxLength(1024);

            builder.Property(n => n.Header)
                .IsUnicode()
                .HasMaxLength(1024);
        }
    }
}

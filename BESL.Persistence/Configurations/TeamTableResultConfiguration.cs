using BESL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BESL.Persistence.Configurations
{
    public class TeamTableResultConfiguration : IEntityTypeConfiguration<TeamTableResult>
    {
        public void Configure(EntityTypeBuilder<TeamTableResult> builder)
        {
            builder.HasOne(x => x.Team)
                .WithMany(x => x.PreviousTeamTableResults)
                .HasForeignKey(x => x.TeamId);
        }
    }
}

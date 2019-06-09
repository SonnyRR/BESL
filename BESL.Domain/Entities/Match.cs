namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Match
    {
        public Guid Id { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int WinnerTeamId { get; set; }
        public Team Winner { get; set; }

        public bool IsDraw { get; set; }

        public DateTime? Date { get; set; }

        public ICollection<Player> ParticipatedPlayers { get; set; } = new HashSet<Player>();

    }
}

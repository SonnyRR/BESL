namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Match : BaseModel<int>
    {

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

        public virtual ICollection<Player> ParticipatedPlayers { get; set; } = new HashSet<Player>();

    }
}

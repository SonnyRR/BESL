namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Match : BaseModel<string>
    {

        public string HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public string AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string WinnerTeamId { get; set; }
        public Team Winner { get; set; }

        public bool IsDraw { get; set; }

        public DateTime? Date { get; set; }

        //public ICollection<Player> ParticipatedPlayers { get; set; } = new HashSet<Player>();

    }
}

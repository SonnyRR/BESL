namespace BESL.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Team : BaseModel<string>
    {

        public string Name { get; set; }

        public string OwnerId { get; set; }
        public Player Owner { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();
        public ICollection<Match> HomeMatches { get; set; } = new HashSet<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new HashSet<Match>();
        public ICollection<Match> WonMatches { get; set; } = new HashSet<Match>();
    }
}

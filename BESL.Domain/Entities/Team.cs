namespace BESL.Domain.Entities
{
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class Team : BaseModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string OwnerId { get; set; }
        public Player Owner { get; set; }
            
        public int CurrentCompetitionId { get; set; }
        public Competition CurrentCompetition { get; set; }

        public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new HashSet<PlayerTeam>();
        public virtual ICollection<Match> HomeMatches { get; set; } = new HashSet<Match>();
        public virtual ICollection<Match> AwayMatches { get; set; } = new HashSet<Match>();
        public virtual ICollection<Match> WonMatches { get; set; } = new HashSet<Match>();
    }
}
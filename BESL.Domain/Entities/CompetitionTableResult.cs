namespace BESL.Domain.Entities
{
    using System.Linq;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class CompetitionTableResult : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int CompetitionTableId { get; set; }
        public CompetitionTable CompetitionTable { get; set; }

        public int MatchesPlayed => this.PlayedMatches
            .Count(m => m.AwayTeamId == this.TeamId || m.HomeTeamId == this.TeamId);

        public int MatchesWon => this.PlayedMatches
            .Count(m => m.WinnerTeamId == this.TeamId);

        public int MatchesLost => this.PlayedMatches
            .Count(m => m.WinnerTeamId != this.TeamId);

        public int TotalPoints { get; set; }

        public int PenaltyPoints { get; set; }

        public virtual ICollection<Match> PlayedMatches { get; set; } = new HashSet<Match>();
    }
}

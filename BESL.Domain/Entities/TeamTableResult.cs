namespace BESL.Domain.Entities
{
    using System.Linq;
    using System.Collections.Generic;

    using BESL.Domain.Infrastructure;

    public class TeamTableResult : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TournamentTableId { get; set; }
        public TournamentTable TournamentTable { get; set; }

        public bool IsDropped { get; set; }

        public bool IsActive { get; set; }

        public int MatchesPlayed => this.PlayedMatches
            .Count();

        public int MatchesWon => this.PlayedMatches
            .Count(m => m.WinnerTeamId == this.TeamId);

        public int MatchesLost => this.PlayedMatches
            .Count(m => m.WinnerTeamId != this.TeamId);

        public int TotalPoints { get; set; }

        public int PenaltyPoints { get; set; }

        public virtual ICollection<Match> PlayedMatches { get; set; } = new HashSet<Match>();
    }
}

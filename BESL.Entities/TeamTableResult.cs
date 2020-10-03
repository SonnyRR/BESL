namespace BESL.Entities
{
    using BESL.Entities.Infrastructure;

    public class TeamTableResult : BaseDeletableModel<int>
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int TournamentTableId { get; set; }
        public TournamentTable TournamentTable { get; set; }

        public bool IsDropped { get; set; }

        public int Points { get; set; }

        public int TotalPoints => this.Points - this.PenaltyPoints;

        public int PenaltyPoints { get; set; }
    }
}

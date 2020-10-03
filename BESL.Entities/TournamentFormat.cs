namespace BESL.Entities
{
    using System.Collections.Generic;
    using BESL.Entities.Infrastructure;

    public class TournamentFormat : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalPlayersCount { get; set; }

        public int TeamPlayersCount { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
        public ICollection<Tournament> Tournaments { get; set; } = new HashSet<Tournament>();
    }
}
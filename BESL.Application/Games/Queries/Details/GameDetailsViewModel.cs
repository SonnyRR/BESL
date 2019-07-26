namespace BESL.Application.Games.Queries.Details
{
    using System.Collections.Generic;

    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public int RegisteredTeams { get; set; }

        public ICollection<CompetitionLookupModel> Tournaments { get; set; }
    }
}

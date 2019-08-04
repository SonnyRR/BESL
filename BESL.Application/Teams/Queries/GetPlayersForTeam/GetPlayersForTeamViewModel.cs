namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using System.Collections.Generic;

    public class GetPlayersForTeamViewModel
    {
        public IEnumerable<PlayerLookup> Players { get; set; }
    }
}

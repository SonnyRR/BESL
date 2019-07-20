namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using System.Collections.Generic;

    public class TeamsForPlayerViewModel
    {
        public ICollection<TeamForPlayerLookupModel> PlayerTeams { get; set; }
    }
}

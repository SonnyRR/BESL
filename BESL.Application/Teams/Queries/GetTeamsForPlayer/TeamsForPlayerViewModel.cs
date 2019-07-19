namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using System.Collections.Generic;

    public class TeamsForPlayerViewModel
    {
        public ICollection<TeamsForPlayerLookupModel> PlayerTeams { get; set; }
    }
}

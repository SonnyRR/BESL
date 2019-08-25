namespace BESL.Application.Teams.Queries.GetAllTeamsPaged
{
    using System.Collections.Generic;

    public class GetAllTeamsPagedViewModel
    {
        public int TotalTeamsCount { get; set; }

        public IEnumerable<TeamLookupModel> Teams { get; set; }
    }
}

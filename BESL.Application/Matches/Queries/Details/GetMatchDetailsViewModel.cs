namespace BESL.Application.Matches.Queries.Details
{
    using BESL.Application.Common.Models.Lookups;

    public class GetMatchDetailsViewModel : MatchLookupModel
    {
        public TeamDetailsLookupModel HomeTeam { get; set; }

        public TeamDetailsLookupModel AwayTeam { get; set; }
    }
}

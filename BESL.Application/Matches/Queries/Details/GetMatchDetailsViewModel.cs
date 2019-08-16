namespace BESL.Application.Matches.Queries.Details
{
    using AutoMapper;
    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GetMatchDetailsViewModel : MatchLookupModel
    {
        public TeamDetailsLookupModel HomeTeam { get; set; }

        public TeamDetailsLookupModel AwayTeam { get; set; }

        public string Week { get; set; }
    }
}

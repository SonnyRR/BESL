namespace BESL.Application.Teams.Queries.GetTeamTournamentsMatches
{
    using AutoMapper;

    using System.Linq;
    using System.Collections.Generic;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class TournamentMatchesLookupModel : IHaveCustomMapping
    {
        public IList<MatchLookupModel> Matches { get; set; }

        public string TournamentName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IGrouping<string, Match>, TournamentMatchesLookupModel>()
                .ForMember(lm => lm.Matches, o => o.MapFrom(src => src))
                .ForMember(lm => lm.TournamentName, o => o.MapFrom(src => src.Key));
        }
    }
}

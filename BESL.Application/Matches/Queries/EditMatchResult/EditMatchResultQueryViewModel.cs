namespace BESL.Application.Matches.Queries.EditMatchResult
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class EditMatchResultQueryViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public int PlayWeekId { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public IEnumerable<PlayerSelectItemLookupModel> TeamPlayers { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Match, EditMatchResultQueryViewModel>()
                .ForMember(x => x.TeamPlayers,
                    o => o.MapFrom(src => src.HomeTeam.PlayerTeams.Where(t => !t.IsDeleted).Concat(src.AwayTeam.PlayerTeams.Where(t => !t.IsDeleted)).Select(x => x.Player)));
        }
    }
}

namespace BESL.Application.Games.Queries.GetGameDetails
{
    using System.Collections.Generic;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GameDetailsLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CompetitionLookupModel> Tournaments { get; set; }

        public int RegisteredTeams { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameDetailsLookupModel>()
                .ForMember(gdlm => gdlm.Tournaments, o => o.MapFrom(g => g.Competitions))
                .ForMember(gdlm => gdlm.RegisteredTeams, o => o.MapFrom(g => g.Teams.Count));
        }
    }
}

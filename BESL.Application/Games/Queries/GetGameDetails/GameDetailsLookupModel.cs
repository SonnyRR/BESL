namespace BESL.Application.Games.Queries.GetGameDetails
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GameDetailsLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameImageUrl { get; set; }

        public int RegisteredTeams { get; set; }

        public ICollection<CompetitionLookupModel> Tournaments { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameDetailsLookupModel>()
                .ForMember(gdlm => gdlm.GameImageUrl, o => o.MapFrom(g => g.GameImageUrl))
                .ForMember(gdlm => gdlm.Tournaments, o => o.MapFrom(g => g.TournamentFormats.SelectMany(t => t.Tournaments)))
                .ForMember(gdlm => gdlm.RegisteredTeams, o => o.MapFrom(g => g.TournamentFormats.Sum(tf => tf.Teams.Count)));
        }
    }
}

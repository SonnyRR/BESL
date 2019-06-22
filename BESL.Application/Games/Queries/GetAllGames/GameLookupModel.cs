namespace BESL.Application.Games.Queries.GetAllGames
{
    using System.Linq;

    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GameLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentActiveTournaments { get; set; }

        public int RegisteredTeams { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameLookupModel>()
                .ForMember(glm=>glm.Description, o => o.MapFrom(g => string.Format("{0}", g.Description.Length > 140 ? $"{g.Description.Substring(0, 140)}..." : g.Description)))
                .ForMember(glm => glm.CurrentActiveTournaments, o => o.MapFrom(g => g.Tournaments.Count(c=>c.IsActive)))
                .ForMember(glm => glm.RegisteredTeams, o => o.MapFrom(g => g.Tournaments.Count));
        }
    }
}

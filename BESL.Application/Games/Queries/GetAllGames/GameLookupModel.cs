namespace BESL.Application.Games.Queries.GetAllGames
{
    using System.Linq;

    using AutoMapper;
    using BESL.Domain.Entities;

    public class GameLookupModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentActiveTournaments { get; set; }

        public int RegisteredTeams { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameLookupModel>()
                .ForMember(glm => glm.CurrentActiveTournaments, o => o.MapFrom(g => g.Competitions.Count(c=>c.IsActive)))
                .ForMember(glm => glm.RegisteredTeams, o => o.MapFrom(g => g.Competitions.Count));
        }
    }
}

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

        public string ImageUrl { get; set; }

        public int CurrentActiveTournaments { get; set; }

        public int RegisteredTeams { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameLookupModel>()
                .ForMember(glm => glm.Description, o => o.MapFrom(g => string.Format("{0}", g.Description.Length > 140 ? $"{g.Description.Substring(0, g.Description.IndexOf('.', 140) == -1 ? 140 : g.Description.IndexOf('.', 140))}..." : g.Description)))
                .ForMember(glm => glm.ImageUrl, o => o.MapFrom(g => g.GameImageUrl))
                .ForMember(glm => glm.CurrentActiveTournaments, o => o.MapFrom(g => g.TournamentFormats.Count(c => c.Tournaments.Any(t => t.IsActive))))
                .ForMember(glm => glm.RegisteredTeams, o => o.MapFrom(g => g.TournamentFormats.Sum(tf => tf.Teams.Count)));
        }
    }
}

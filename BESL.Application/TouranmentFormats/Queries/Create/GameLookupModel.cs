using AutoMapper;
using BESL.Application.Interfaces.Mapping;
using BESL.Domain.Entities;

namespace BESL.Application.TournamentFormats.Queries.Create
{
    public class GameLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameLookupModel>();
        }
    }
}

namespace BESL.Application.Games.Queries.GetAllGamesSelectList
{ 
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class GameSelectItemLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Game, GameSelectItemLookupModel>();
        }
    }
}

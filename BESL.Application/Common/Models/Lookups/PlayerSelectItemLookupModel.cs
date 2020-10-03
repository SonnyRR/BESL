namespace BESL.Application.Common.Models.Lookups
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Entities;

    public class PlayerSelectItemLookupModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Player, PlayerSelectItemLookupModel>();
        }
    }
}

namespace BESL.Application.Tournaments.Models
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentFormatSelectListLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentFormat, TournamentFormatSelectListLookupModel>()
                .ForMember(tflm => tflm.Name, o => o.MapFrom(tfdm => $"{tfdm.Name} - {tfdm.Game.Name}"));
        }
    }
}

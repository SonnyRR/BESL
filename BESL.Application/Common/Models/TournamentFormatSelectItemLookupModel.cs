namespace BESL.Application.Common.Models
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentFormatSelectItemLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TournamentFormat, TournamentFormatSelectItemLookupModel>()
                .ForMember(tfs => tfs.Name, o => o.MapFrom(tf => $"{tf.Name} - {tf.Game.Name}"));
        }
    }
}

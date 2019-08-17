namespace BESL.Application.Tournaments.Queries.GetAllTournaments
{
    using AutoMapper;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class TournamentLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Game { get; set; }

        public string Format { get; set; }

        public string TournamentImageUrl { get; set; }

        public string Description { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Tournament, TournamentLookupModel>()
                .ForMember(tlm => tlm.Game, o => o.MapFrom(t => t.Format.Game.Name))
                .ForMember(tlm => tlm.Format, o => o.MapFrom(t => t.Format.Name))
                .ForMember(tlm => tlm.Description, o => o.MapFrom(t => string.Format("{0}", t.Description.Length > 140 ? $"{t.Description.Substring(0, t.Description.IndexOf('.', 140) == -1 ? 140 : t.Description.IndexOf('.', 140))}..." : t.Description)));
        }
    }
}

using AutoMapper;
using BESL.Application.Interfaces.Mapping;
using BESL.Domain.Entities;
using static BESL.Common.GlobalConstants;
namespace BESL.Application.Tournaments.Queries.GetTournamentDetails
{
    public class GetTournamentDetailsViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TournamentImageUrl { get; set; }

        public bool IsActive { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Format { get; set; }

        public string Game { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Tournament, GetTournamentDetailsViewModel>()
                .ForMember(vm => vm.StartDate, o => o.MapFrom(src => src.StartDate.ToString(DATE_FORMAT)))
                .ForMember(vm => vm.EndDate, o => o.MapFrom(src => src.EndDate.ToString(DATE_FORMAT)))
                .ForMember(vm => vm.Format, o => o.MapFrom(src => src.Format.Name));
                //.ForMember(vm => vm.;
        }
    }
}

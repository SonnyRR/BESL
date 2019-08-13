namespace BESL.Application.Common.Models.Lookups
{
    using System.Linq;
    using AutoMapper;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    using static BESL.Common.GlobalConstants;

    public class TeamDetailsLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string TeamImageUrl { get; set; }

        public string CreatedOn { get; set; }

        public bool IsOwner { get; set; }

        public bool IsMember { get; set; }

        public string TournamentFormat { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Team, TeamDetailsLookupModel>()
                .ForMember(vm => vm.TeamImageUrl, o => o.MapFrom(src => src.ImageUrl))
                .ForMember(vm => vm.CreatedOn, o => o.MapFrom(src => src.CreatedOn.ToString(DATE_FORMAT)))
                .ForMember(vm => vm.TournamentFormat, o => o.MapFrom(src => $"{src.TournamentFormat.Game.Name} - {src.TournamentFormat.Name}"))
                .ForMember(vm => vm.IsOwner, o => o.MapFrom((src, opt, destMember, context) => src.OwnerId == (string)context.Items["CurrentUserId"]))
                .ForMember(vm => vm.IsMember, o => o.MapFrom((src, opt, destMember, context) => src.PlayerTeams.Any(x => x.PlayerId == (string)context.Items["CurrentUserId"] && x.IsDeleted == false)));
        }
    }
}

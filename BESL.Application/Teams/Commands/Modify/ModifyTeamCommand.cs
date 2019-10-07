namespace BESL.Application.Teams.Commands.Modify
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class ModifyTeamCommand : IRequest<int>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string ImageUrl { get; set; }

        public string FormatName { get; set; }

        public IFormFile TeamImage { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Team, ModifyTeamCommand>()
                .ForMember(vm => vm.FormatName, o => o.MapFrom(src => $"{src.TournamentFormat.Name} - {src.TournamentFormat.Game.Name}"))
                .ForMember(vm => vm.HomepageUrl, o => o.MapFrom(src => src.HomepageUrl));
        }
    }
}

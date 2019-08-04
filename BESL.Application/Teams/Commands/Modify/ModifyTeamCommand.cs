namespace BESL.Application.Teams.Commands.Modify
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class ModifyTeamCommand : IRequest<int>, IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile TeamImage { get; set; }
    }
}

namespace BESL.Application.Teams.Commands.Create
{
    using System.Collections.Generic;

    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Common.Models.Lookups;
    using BESL.Application.Interfaces.Mapping;
    using BESL.Domain.Entities;

    public class CreateTeamCommand : IRequest<int>, IMapTo<Team>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string OwnerId { get; set; }

        public int TournamentFormatId { get; set; }
        public IEnumerable<TournamentFormatSelectItemLookupModel> Formats { get; set; }

        public IFormFile TeamImage { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateTeamCommand, Team>();
        }
    }
}

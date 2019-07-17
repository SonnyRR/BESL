namespace BESL.Application.Teams.Commands.Create
{
    using BESL.Application.Common.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class CreateTeamCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string HomepageUrl { get; set; }

        public string OwnerId { get; set; }

        public int TournamentFormatId { get; set; }
        public IEnumerable<TournamentFormatSelectItemLookupModel> Formats { get; set; }

        public IFormFile TeamImage { get; set; }

    }
}

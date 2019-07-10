namespace BESL.Application.Tournaments.Commands.Create
{
    using System.Collections.Generic;

    using MediatR;
    using Microsoft.AspNetCore.Http;

    using BESL.Application.Tournaments.Models;
    using System;

    public class CreateTournamentCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile TournamentImage { get; set; }

        public int FormatId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<TournamentFormatSelectListLookupModel> Formats { get; set; }
    }
}

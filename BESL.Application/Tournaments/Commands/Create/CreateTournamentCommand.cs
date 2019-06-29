namespace BESL.Application.Tournaments.Commands.Create
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateTournamentCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile TournamentImage { get; set; }

        public int FormatId { get; set; }
    }
}

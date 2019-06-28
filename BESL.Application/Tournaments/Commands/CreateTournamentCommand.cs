namespace BESL.Application.Tournaments.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateTournamentCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile GameImage { get; set; }

        public int FormatId { get; set; }
    }
}

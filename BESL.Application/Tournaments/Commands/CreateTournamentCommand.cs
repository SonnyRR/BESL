namespace BESL.Application.Tournaments.Commands
{
    using MediatR;

    public class CreateTournamentCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int FormatId { get; set; }
    }
}

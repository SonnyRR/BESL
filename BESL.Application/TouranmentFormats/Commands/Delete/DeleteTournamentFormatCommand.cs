namespace BESL.Application.TournamentFormats.Commands.Delete
{
    using MediatR;

    public class DeleteTournamentFormatCommand : IRequest
    {
        public int Id { get; set; }

        public string FormatName { get; set; }
    }
}

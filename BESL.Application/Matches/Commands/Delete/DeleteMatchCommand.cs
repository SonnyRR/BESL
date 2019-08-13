namespace BESL.Application.Matches.Commands.Delete
{
    using MediatR;

    public class DeleteMatchCommand
    {
        public int MatchId { get; set; }
    }
}

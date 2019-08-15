namespace BESL.Application.TeamTableResults.Commands.AddPoints
{
    using MediatR;

    public class AddPointsCommand : IRequest<int>
    {
        public int TeamId { get; set; }

        public int TournamentId { get; set; }

        public int Points { get; set; }
    }
}

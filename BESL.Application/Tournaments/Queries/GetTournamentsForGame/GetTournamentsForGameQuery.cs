namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using MediatR;

    public class GetTournamentsForGameQuery : IRequest<GameTournamentsViewModel>
    {
        public int GameId { get; set; }
    }
}

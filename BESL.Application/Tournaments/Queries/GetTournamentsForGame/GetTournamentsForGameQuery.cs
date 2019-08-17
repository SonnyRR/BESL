namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using MediatR;
    using BESL.Application.Tournaments.Queries.Common;

    public class GetTournamentsForGameQuery : IRequest<AllTournamentsViewModel>
    {
        public int GameId { get; set; }
    }
}

namespace BESL.Application.Tournaments.Queries.GetTournamentsForGame
{
    using MediatR;
    using BESL.Application.Common.Models.View;

    public class GetTournamentsForGameQuery : IRequest<AllTournamentsViewModel>
    {
        public int GameId { get; set; }
    }
}

using MediatR;

namespace BESL.Application.Games.Queries.GetGameDetails
{
    public class GameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public int Id { get; set; }
    }
}

namespace BESL.Application.Games.Queries.GetGameDetails
{
    using MediatR;

    public class GameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public int Id { get; set; }
    }
}

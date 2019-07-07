namespace BESL.Application.Games.Queries.GetGameDetails
{
    using MediatR;

    public class GetGameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public int Id { get; set; }
    }
}

namespace BESL.Application.Games.Queries.Details
{
    using MediatR;

    public class GetGameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public int Id { get; set; }
    }
}

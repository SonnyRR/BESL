namespace BESL.Application.Players.Queries.PlayerDetails
{
    using MediatR;

    public class PlayerDetailsQuery : IRequest<PlayerDetailsViewModel>
    {
        public string Username { get; set; }
    }
}

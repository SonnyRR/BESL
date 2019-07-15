namespace BESL.Application.Players.Queries.GetPlayerDetails
{
    using MediatR;

    public class GetPlayerDetailsQuery : IRequest<PlayerDetailsViewModel>
    {
        public string Username { get; set; }
    }
}

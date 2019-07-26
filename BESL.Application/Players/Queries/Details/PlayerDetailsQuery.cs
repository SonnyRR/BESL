namespace BESL.Application.Players.Queries.Details
{
    using MediatR;

    public class GetPlayerDetailsQuery : IRequest<PlayerDetailsViewModel>
    {
        public string Username { get; set; }
    }
}

namespace BESL.Application.Players.Queries.GetMatchParticipatedPlayers
{
    using MediatR;

    public class GetMatchParticipatedPlayersQuery : IRequest<MatchParticipatedPlayersViewModel>
    {
        public int MatchId { get; set; }
    }
}

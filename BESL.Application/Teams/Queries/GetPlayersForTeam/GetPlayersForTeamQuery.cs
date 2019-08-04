namespace BESL.Application.Teams.Queries.GetPlayersForTeam
{
    using MediatR;

    public class GetPlayersForTeamQuery : IRequest<GetPlayersForTeamViewModel>
    {
        public int TeamId { get; set; }
    }
}

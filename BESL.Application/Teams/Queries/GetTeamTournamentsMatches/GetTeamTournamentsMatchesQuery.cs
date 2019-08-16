namespace BESL.Application.Teams.Queries.GetTeamTournamentsMatches
{
    using MediatR;

    public class GetTeamTournamentsMatchesQuery : IRequest<GetTeamTournamentsMatchesViewModel>
    {
        public int TeamId { get; set; }
    }
}

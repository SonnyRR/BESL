namespace BESL.Application.Players.Queries.Details
{
    using MediatR;

    public class GetTeamsForPlayerQuery : IRequest<TeamsForPlayerViewModel>
    {
        public string UserId { get; set; }
    }
}

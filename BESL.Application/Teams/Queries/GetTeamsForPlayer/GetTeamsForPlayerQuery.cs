namespace BESL.Application.Teams.Queries.GetTeamsForPlayer
{
    using MediatR;

    public class GetTeamsForPlayerQuery : IRequest<TeamsForPlayerViewModel>
    {
        public string UserId { get; set; }

        public bool WithDeleted { get; set; }
    }
}

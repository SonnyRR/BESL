namespace BESL.Application.Teams.Queries.GetAllTeamsPaged
{
    using MediatR;

    public class GetAllTeamsPagedQuery : IRequest<GetAllTeamsPagedViewModel>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}

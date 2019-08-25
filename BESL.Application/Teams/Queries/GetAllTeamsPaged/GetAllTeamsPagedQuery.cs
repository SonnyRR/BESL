namespace BESL.Application.Teams.Queries.GetAllTeamsPaged
{
    using System.Collections.Generic;
    using MediatR;

    public class GetAllTeamsPagedQuery : IRequest<GetAllTeamsPagedViewModel>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}

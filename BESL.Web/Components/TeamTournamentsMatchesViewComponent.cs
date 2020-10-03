namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.GetTeamTournamentsMatches;

    public class TeamTournamentsMatchesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public TeamTournamentsMatchesViewComponent(IMediator mediator) => this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
            => this.View(await this.mediator.Send(new GetTeamTournamentsMatchesQuery { TeamId = teamId }));
    }
}

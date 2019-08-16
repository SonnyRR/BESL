namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.GetTeamTournamentsMatches;

    public class GetTeamTournamentsMatchesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GetTeamTournamentsMatchesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var viewModel = await this.mediator.Send(new GetTeamTournamentsMatchesQuery { TeamId = teamId });
            return this.View(viewModel);
        }
    }
}

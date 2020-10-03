namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.GetPlayersForTeam;

    public class PlayersForTeamViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public PlayersForTeamViewComponent(IMediator mediator) => this.mediator = mediator;
        
        public async Task<IViewComponentResult> InvokeAsync(int teamId)
            => this.View(await this.mediator.Send(new GetPlayersForTeamQuery { TeamId = teamId }));
    }
}

namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.GetTeamsForPlayer;

    public class PlayerDetailsTeamsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public PlayerDetailsTeamsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetTeamsForPlayerQuery { UserId = userId, WithDeleted = true});
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}

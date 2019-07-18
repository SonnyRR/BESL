namespace BESL.Web.Components
{
    using BESL.Application.Tournaments.Queries.GetTournamentsForGame;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ActiveTournamentsTableViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ActiveTournamentsTableViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int gameId)
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetTournamentsForGameQuery() { GameId = gameId });
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}

namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class ActiveTournamentsTableViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ActiveTournamentsTableViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetAllTournamentsQuery());
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}

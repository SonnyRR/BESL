namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using BESL.Application.Tournaments.Queries.GetAllTournamentsSelectList;

    public class ActiveTournamentsDropDownViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ActiveTournamentsDropDownViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetAllTournamentsSelectListQuery());
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}

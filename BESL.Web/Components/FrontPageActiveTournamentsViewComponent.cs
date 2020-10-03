namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Queries.GetAllTournaments;

    public class FrontPageActiveTournamentsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public FrontPageActiveTournamentsViewComponent(IMediator mediator) => this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync()
            => this.View(await this.mediator.Send(new GetAllTournamentsQuery()));
    }
}

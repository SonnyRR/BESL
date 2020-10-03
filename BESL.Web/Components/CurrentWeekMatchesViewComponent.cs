namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Matches.Queries.GetMatchesForCurrentPlayWeeks;

    public class CurrentWeekMatchesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public CurrentWeekMatchesViewComponent(IMediator mediator) => this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync()
            => this.View(await this.mediator.Send(new GetMatchesForCurrentPlayWeeksQuery()));
    }
}

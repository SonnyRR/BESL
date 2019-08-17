namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Matches.Queries.GetMatchesForCurrentPlayWeeks;

    public class GetCurrentWeekMatchesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GetCurrentWeekMatchesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = await this.mediator.Send(new GetMatchesForCurrentPlayWeeksQuery());
            return this.View(viewModel);
        }
    }
}

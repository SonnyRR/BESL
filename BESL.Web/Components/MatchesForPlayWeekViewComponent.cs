namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;

    public class MatchesForPlayWeekViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public MatchesForPlayWeekViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int playWeekId)
        {
            var viewModel = await this.mediator.Send(new GetMatchesForPlayWeekQuery { PlayWeekId = playWeekId });
            return this.View(viewModel);
        }
    }
}

namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using BESL.Application.PlayWeeks.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class TableWeeksNavigationViewComponent : ViewComponent
    {
        private IMediator mediator;

        public TableWeeksNavigationViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tournamentTableId)
        {
            var viewModel = await this.mediator.Send(new GetPlayWeeksForTournamentTableQuery { TournamentTableId = tournamentTableId });
            return this.View(viewModel);
        }
    }
}

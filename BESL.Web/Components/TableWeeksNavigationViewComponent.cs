namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable;

    public class TableWeeksNavigationViewComponent : ViewComponent
    {
        private IMediator mediator;

        public TableWeeksNavigationViewComponent(IMediator mediator) => this.mediator = mediator;

        public async Task<IViewComponentResult> InvokeAsync(int tournamentTableId)
            => this.View(await this.mediator.Send(new GetPlayWeeksForTournamentTableQuery { TournamentTableId = tournamentTableId }));
    }
}

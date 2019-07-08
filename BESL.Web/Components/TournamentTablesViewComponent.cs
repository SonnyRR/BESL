namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TournamentTables.Queries.GetTournamentTables;

    [ViewComponent(Name = nameof(TournamentTablesViewComponent))]
    public class TournamentTablesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public TournamentTablesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tournamentId)
        {
            var model = await this.mediator.Send(new GetTournamentTablesQuery() { TournamentId = tournamentId });
            return this.View(model);
        }
    }
}

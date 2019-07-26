namespace BESL.Web.Components
{
    using System;
    using System.Threading.Tasks;
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class TournamentSkillTablesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public TournamentSkillTablesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tournamentId)
        {
            var currentActiveTournamentsViewModel = await this.mediator.Send(new GetTournamentTablesQuery() { Id = tournamentId });
            return this.View(currentActiveTournamentsViewModel);
        }
    }
}

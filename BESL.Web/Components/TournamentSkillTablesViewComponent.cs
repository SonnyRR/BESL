namespace BESL.Web.Components
{
    using System.Threading.Tasks;
 
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
 
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;

    public class TournamentSkillTablesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public TournamentSkillTablesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tournamentId)
        {
            var viewModel = await this.mediator.Send(new GetTournamentTablesQuery() { Id = tournamentId });
            return this.View(viewModel);
        }
    }
}

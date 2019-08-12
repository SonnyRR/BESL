﻿namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TournamentTables.Queries.GetTeamsForTournamentTable;

    public class AddMatchFixtureViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public AddMatchFixtureViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int tournamentTableId)
        {
            var viewModel = await this.mediator.Send(new GetTeamsForTournamentTableQuery { TournamentTableId = tournamentTableId });
            return this.View(viewModel);
        }
    }
}

﻿namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Queries.GetTournamentsForGame;

    public class GameTournamentsTableViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GameTournamentsTableViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int gameId)
        {
            var viewModel = await this.mediator.Send(new GetTournamentsForGameQuery() { GameId = gameId });
            return this.View(viewModel);
        }
    }
}

﻿namespace BESL.Web.Components
{
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Queries.GetPlayersForTeam;

    public class TeamModifyPlayersViewComponent : ViewComponent
    {
        public IMediator mediator;

        public TeamModifyPlayersViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var viewModel = await this.mediator.Send(new GetPlayersForTeamQuery { TeamId = teamId });
            return this.View(viewModel);
        }

    }
}

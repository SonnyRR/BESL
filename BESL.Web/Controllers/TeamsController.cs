﻿namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Commands.Create;
    using BESL.Application.Teams.Queries.Create;
    using System.Security.Claims;
    using BESL.Application.Teams.Queries.Details;
    using BESL.Application.Interfaces;

    public class TeamsController : BaseController
    {
        private readonly IUserAcessor userAcessor;

        public TeamsController(IUserAcessor userAcessor)
        {
            this.userAcessor = userAcessor;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var viewModel = await this.Mediator.Send(new CreateTeamQuery());
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTeamCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Formats = (await this.Mediator.Send(new CreateTeamQuery())).Formats;
                return this.View(command);
            }

            command.OwnerId = this.userAcessor.UserId;
            var assignedId = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), assignedId);
        }

        public async Task<IActionResult> Details(GetTeamDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }
    }
}

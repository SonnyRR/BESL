namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Commands.Create;
    using BESL.Application.Teams.Commands.Modify;
    using BESL.Application.Teams.Commands.InvitePlayer;
    using BESL.Application.Teams.Commands.RemovePlayer;
    using BESL.Application.Teams.Commands.TransferOwnership;
    using BESL.Application.Teams.Queries.Details;
    using BESL.Application.Teams.Queries.Modify;
    using BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList;

    [Authorize]
    public class TeamsController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateTeamCommand { Formats = await this.Mediator.Send(new GetAllTournamentFormatsSelectListQuery()) };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Formats = await this.Mediator.Send(new GetAllTournamentFormatsSelectListQuery());
                return this.View(command);
            }

            var teamId = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new { Id = teamId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(GetTeamDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Modify(ModifyTeamQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTeamCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var viewModel = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetTeamDetailsQuery { Id = command.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Invite(InvitePlayerCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Modify), new ModifyTeamQuery { Id = command.TeamId });
        }

        [HttpPost]
        public async Task<IActionResult> RemovePlayer(RemovePlayerCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Modify), new ModifyTeamQuery { Id = command.TeamId });
        }

        public async Task<IActionResult> Leave(RemovePlayerCommand command)
        {
            await this.Mediator.Send(command);
            return this.LocalRedirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> TransferOwnership(TransferTeamOwnershipCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetTeamDetailsQuery { Id = command.TeamId });
        }
    }
}

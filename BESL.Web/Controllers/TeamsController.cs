namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Interfaces;
    using BESL.Application.Teams.Commands.Create;
    using BESL.Application.Teams.Queries.Create;
    using BESL.Application.Teams.Queries.Details;
    using BESL.Application.Teams.Queries.Modify;
    using BESL.Application.Teams.Commands.Modify;
    using BESL.Application.Teams.Commands.InvitePlayer;
    using BESL.Application.Teams.Commands.RemovePlayer;

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
            var teamId = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new { Id = teamId });
        }

        public async Task<IActionResult> Details(GetTeamDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Modify(ModifyTeamQuery query)
        {
            query.CurrentLoggedInUserId = this.userAcessor.UserId;
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTeamCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var viewModel = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Modify), new GetTeamDetailsQuery { Id = command.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Invite(InvitePlayerCommand command)
        {
            command.SenderUsername = this.userAcessor.User.Identity.Name;
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Modify), new ModifyTeamQuery { Id = command.TeamId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemovePlayer(RemovePlayerCommand command)
        {
            command.CurrentUserId = this.userAcessor.UserId;
            await this.Mediator.Send(command);

            return this.RedirectToAction(nameof(Details), new GetTeamDetailsQuery { Id = command.TeamId });
        }
    }
}

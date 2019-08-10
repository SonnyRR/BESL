namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using BESL.Application.Players.Queries.Details;
    using BESL.Application.Players.Queries.Invites;
    using BESL.Application.Players.Commands.AcceptInvite;
    using BESL.Application.Players.Commands.DeclineInvite;

    public class PlayersController : BaseController
    {
        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.Mediator.Send(new GetPlayerDetailsQuery() { Username = id });
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Invites()
        {
            var viewModel = await this.Mediator.Send(new GetInvitesForPlayerQuery());
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AcceptInvite(AcceptInviteCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Invites));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeclineInvite(DeclineInviteCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Invites));
        }
    }
}

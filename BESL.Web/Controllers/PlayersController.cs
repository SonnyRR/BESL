namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Interfaces;
    using BESL.Application.Players.Queries.Details;
    using BESL.Application.Players.Queries.Invites;
    using Microsoft.AspNetCore.Authorization;
    using BESL.Application.Players.Commands.AcceptInvite;

    public class PlayersController : BaseController
    {
        private readonly IUserAcessor userAcessor;

        public PlayersController(IUserAcessor userAcessor)
        {
            this.userAcessor = userAcessor;
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.Mediator.Send(new GetPlayerDetailsQuery() { Username = id });
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Invites()
        {
            var viewModel = await this.Mediator.Send(new GetInvitesForPlayerQuery {  UserId = this.userAcessor.UserId });
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AcceptInvite(string id)
        {
            await this.Mediator.Send(new AcceptInviteCommand { InviteId = id, UserId = this.userAcessor.UserId });
            return this.RedirectToAction(nameof(Invites));
        }
    }
}

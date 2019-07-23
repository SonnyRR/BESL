namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Teams.Commands.Create;
    using BESL.Application.Teams.Queries.Create;
    using BESL.Domain.Entities;


    public class TeamsController : BaseController
    {
        private readonly UserManager<Player> userManager;

        public TeamsController(UserManager<Player> userManager)
        {
            this.userManager = userManager;
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
            command.OwnerId = this.userManager.GetUserId(this.User);
            await this.Mediator.Send(command);

            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index(string id)
        {
            return this.View();
        }

        public async Task<IActionResult> Details(int id)
        {
            return this.View();
        }
    }
}

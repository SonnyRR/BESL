namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Web.Filters;
    using BESL.Application.Games.Queries.GetGameDetails;
    using Microsoft.Extensions.Configuration;
    using BESL.Application.Games.Commands.Delete;

    [AjaxOnlyFilter]
    public class GamesController : AdminController
    {
        private readonly IConfiguration configuration;

        public GamesController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }      

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            await this.Mediator.Send(command);

            return View();
        }

        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            await this.NotifyService.SendUserSuccessNotificationAsync("testmsg", this.UserNameIdentifier);
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.Mediator.Send(new GameDetailsQuery() { Id = id });
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteGameCommand command)
        {
            var isDeleteSuccessfull = await this.Mediator.Send(command);
            return this.Redirect("/Administration/Games/All");
        }
    }
}

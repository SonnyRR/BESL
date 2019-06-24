namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Games.Commands.CreateGame;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Web.Filters;
    using BESL.Application.Games.Queries.GetGameDetails;
    using System.Threading;

    [AjaxOnlyFilter]
    public class GamesController : AdminController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand command)
        {
            //await this.Mediator.Send(command);

            return View();
        }

        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.Mediator.Send(new GameDetailsQuery() { Id = id });
            return this.View(model);
        }
    }
}

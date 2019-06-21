namespace BESL.Web.Controllers
{
    using BESL.Application.Games.Commands.CreateGame;
    using BESL.Application.Games.Queries.GetAllGames;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class GamesController : BaseController
    {

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameCommand command)
        {
            await this.Mediator.Send(command);

            return View();
        }


        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }
    }
}

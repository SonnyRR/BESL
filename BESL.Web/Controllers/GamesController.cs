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

        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            return this.View("Home/Index");
        }
    }
}

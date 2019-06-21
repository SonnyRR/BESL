namespace BESL.Web.Controllers
{
    using BESL.Application.Games.Commands.CreateGame;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class GameController : BaseController
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
        public async Task<IActionResult> Create([FromForm] CreateGameCommand command)
        {
            await this.Mediator.Send(command);

            return View();
        }
    }
}

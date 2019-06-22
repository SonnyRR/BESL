namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using BESL.Application.Games.Commands.CreateGame;
    using BESL.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : BaseController
    {
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
    }
}

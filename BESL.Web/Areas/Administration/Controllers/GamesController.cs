namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
 
    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.Modify;
    
    public class GamesController : AdminController
    {
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
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(viewModel);
        }

        public async Task<IActionResult> Modify(ModifyGameQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyGameCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteGameCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }
    }
}

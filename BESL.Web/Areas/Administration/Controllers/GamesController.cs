namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.ModifyGame;
    using static BESL.Common.GlobalConstants;
    using BESL.Web.Filters;

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
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            int gameId = await this.Mediator.Send(command);
            this.NotifyService.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);

            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Modify(int id)
        {
            var model = await this.Mediator.Send(new ModifyGameQuery() { Id = id });
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyGameCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                var model = await this.Mediator.Send(new ModifyGameQuery() { Id = command.Id });

                return this.View(model);
            }

            await this.Mediator.Send(command);

            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteGameCommand command)
        {
            var isDeleteSuccessfull = await this.Mediator.Send(command);

            this.NotifyService.SendUserSuccessNotificationAsync(command.GameName, DELETED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction(nameof(All));
        }
    }
}

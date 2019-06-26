﻿namespace BESL.Web.Areas.Administration.Controllers
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

            int? gameId = await this.Mediator.Send(command);

            if (gameId != null)
            {
                await this.NotifyService.SendUserSuccessNotificationAsync($"{command.Name}", "has been created successfuly!", this.UserNameIdentifier);
            }
            else
            {
                await this.NotifyService.SendUserFailiureNotificationAsync("An error has occured!", $"{command.Name} could not be created!", this.UserNameIdentifier);
            }

            return this.RedirectToAction("All");
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

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteGameCommand command)
        {
            var isDeleteSuccessfull = await this.Mediator.Send(command);

            if (isDeleteSuccessfull)
            {
                await this.NotifyService.SendUserSuccessNotificationAsync(command.GameName, $"has been deleted!", this.UserNameIdentifier);
            }
            else
            {
                await this.NotifyService.SendUserFailiureNotificationAsync("An error has occured!", $"{command.GameName} cound not be deleted!", this.UserNameIdentifier);
            }

            return this.Redirect("/Administration/Games/All");
        }
    }
}

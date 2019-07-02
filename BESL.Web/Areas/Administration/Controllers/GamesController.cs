﻿namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using BESL.Application.Games.Commands.Create;
    using BESL.Application.Games.Commands.Delete;
    using BESL.Application.Games.Commands.Modify;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.ModifyGame;
    using BESL.Web.Filters;

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

            int gameId = await this.Mediator.Send(command);
            this.NotifyService.SendUserSuccessNotificationAsync(command.Name, "has beed created successfully!", this.UserNameIdentifier);

            return this.RedirectToAction("All");
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

            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteGameCommand command)
        {
            var isDeleteSuccessfull = await this.Mediator.Send(command);

            this.NotifyService.SendUserSuccessNotificationAsync(command.GameName, $"has been deleted!", this.UserNameIdentifier);
            return this.Redirect("/Administration/Games/All");
        }
    }
}

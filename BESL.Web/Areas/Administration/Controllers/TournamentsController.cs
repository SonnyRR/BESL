namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Tournaments.Commands.Create;
    using Microsoft.AspNetCore.Mvc;
    using static BESL.Common.GlobalConstants;

    public class TournamentsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
           return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create");
            }

            await this.Mediator.Send(command);
            this.NotifyService.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            return this.View(new GamesListViewModel() { Games = new List<GameLookupModel>()});
        }
    }
}

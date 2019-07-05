namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Application.Tournaments.Queries.Create;
    using Microsoft.AspNetCore.Mvc;
    using static BESL.Common.GlobalConstants;

    public class TournamentsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
            var model = await this.Mediator.Send(new CreateTournamentQuery());
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Create));
            }

            await this.Mediator.Send(command);

            _ = this.NotifyService.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            return NoContent();
        }
    }
}

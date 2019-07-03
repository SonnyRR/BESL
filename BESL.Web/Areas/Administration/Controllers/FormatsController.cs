﻿namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Formats.Queries.Create;
    using BESL.Application.Formats.Commands.Create;
    using BESL.Application.Formats.Queries.GetAll;
    using BESL.Application.Formats.Commands.Delete;
    using static BESL.Common.GlobalConstants;

    public class FormatsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
            var viewModel = await this.Mediator.Send(new CreateTournamentFormatQuery());
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormatCommand command)
        {
            var tournamentFormatId = await this.Mediator.Send(command);

            this.NotifyService.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var tournamentFormats = await this.Mediator.Send(new GetAllTournamentFormatsQuery()); 
            return this.View(tournamentFormats);
        }

        public async Task<IActionResult> Delete(DeleteTournamentFormatCommand command)
        {
            await this.Mediator.Send(command);

            this.NotifyService.SendUserSuccessNotificationAsync(command.FormatName, DELETED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.Redirect("All");
        }
    }
}

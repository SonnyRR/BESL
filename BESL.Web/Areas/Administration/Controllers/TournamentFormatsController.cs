namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TournamentFormats.Queries.Create;
    using BESL.Application.TournamentFormats.Commands.Create;
    using BESL.Application.TournamentFormats.Queries.GetAll;
    using BESL.Application.TournamentFormats.Commands.Delete;
    using static BESL.Common.GlobalConstants;
    using BESL.Application.TournamentFormats.Queries.Modify;
    using BESL.Application.TournamentFormats.Commands.Modify;

    public class TournamentFormatsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
            var viewModel = await this.Mediator.Send(new CreateTournamentFormatQuery());
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormatCommand command)
        {
            await this.Mediator.Send(command);

            _ = this.NotifyService.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Modify(ModifyTournamentFormatQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTournamentFormatCommand command)
        {
            var viewModel = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var tournamentFormats = await this.Mediator.Send(new GetAllTournamentFormatsQuery()); 
            return this.View(tournamentFormats);
        }

        public async Task<IActionResult> Delete(DeleteTournamentFormatCommand command)
        {
            await this.Mediator.Send(command);

            _ = this.NotifyService.SendUserSuccessNotificationAsync(command.FormatName, DELETED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction(nameof(All));
        }
    }
}
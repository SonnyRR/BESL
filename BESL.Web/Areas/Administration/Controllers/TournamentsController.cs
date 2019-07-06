namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Application.Tournaments.Queries.Create;
    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;
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
            var model = await this.Mediator.Send(new GetAllTournamentsQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Tables(int id)
        {
            var viewModel = await this.Mediator.Send(new GetTournamentTablesQuery() { TournamentId = id });
            return this.View(viewModel);
        }
    }
}

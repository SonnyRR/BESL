namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Application.Tournaments.Queries.Create;
    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using static BESL.Common.GlobalConstants;
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;

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
                var model = await this.Mediator.Send(new CreateTournamentQuery());
                return this.View(model);
            }

            await this.Mediator.Send(command);

            _ = this.UserNotificationHub.SendUserSuccessNotificationAsync(command.Name, CREATED_SUCCESSFULLY_MSG, this.UserNameIdentifier);
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
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

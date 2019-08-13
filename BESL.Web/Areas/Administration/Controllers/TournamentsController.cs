namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Commands.Create;
    using BESL.Application.Tournaments.Commands.Delete;
    using BESL.Application.Tournaments.Commands.Modify;
    using BESL.Application.Tournaments.Queries.Modify;
    using BESL.Application.Tournaments.Queries.GetAllTournaments;
    using BESL.Application.TournamentFormats.Queries.GetAllTournamentFormatsSelectList;
    using BESL.Application.TournamentTables.Queries.GetTournamentTables;

    public class TournamentsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
            var model = new CreateTournamentCommand { Formats = await this.Mediator.Send(new GetAllTournamentFormatsSelectListQuery()) };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Formats = await this.Mediator.Send(new GetAllTournamentFormatsSelectListQuery());
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTournamentCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.Mediator.Send(new GetAllTournamentsQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Tables(GetTournamentTablesQuery query)
        {
            var viewModel = await this.Mediator.Send(query);

            this.ViewData["ReturnUrl"] = this.RedirectToAction(nameof(Tables), new GetTournamentTablesQuery { Id = query.Id })
                .UrlHelper
                .Action();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Modify(TournamentModifyQuery query)
        {
            var model = await this.Mediator.Send(query);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTournamentCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var model = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }
    }
}

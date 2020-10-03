namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TournamentFormats.Commands.Create;
    using BESL.Application.TournamentFormats.Commands.Delete;
    using BESL.Application.TournamentFormats.Commands.Modify;
    using BESL.Application.TournamentFormats.Queries.GetAllTournamentFormats;
    using BESL.Application.TournamentFormats.Queries.Modify;
    using BESL.Application.Games.Queries.GetAllGamesSelectList;

    public class TournamentFormatsController : AdminController
    {
        public async Task<IActionResult> Create()
            => this.View(new CreateTournamentFormatCommand { Games = await this.Mediator.Send(new GetAllGamesSelectListQuery()) });

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormatCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Games = await this.Mediator.Send(new GetAllGamesSelectListQuery());
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Modify(ModifyTournamentFormatQuery query)
            => this.View(await this.Mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyTournamentFormatCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
            => this.View(await this.Mediator.Send(new GetAllTournamentFormatsQuery()));

        public async Task<IActionResult> Delete(DeleteTournamentFormatCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
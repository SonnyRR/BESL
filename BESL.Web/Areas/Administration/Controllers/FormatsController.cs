namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Formats.Queries.Create;
    using BESL.Application.Formats.Commands.Create;
    using BESL.Application.Formats.Queries.GetAll;

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
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            var tournamentFormats = await this.Mediator.Send(new GetAllTournamentFormatsQuery()); 
            return this.View(tournamentFormats);
        }
    }
}

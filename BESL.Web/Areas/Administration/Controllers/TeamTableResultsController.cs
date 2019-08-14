namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TeamTableResults.Commands.Activate;
    using BESL.Application.TeamTableResults.Commands.AddPenaltyPoints;
    using BESL.Application.TeamTableResults.Commands.Drop;

    public class TeamTableResultsController : AdminController
    {
        [HttpPost]
        public async Task<IActionResult> Drop(DropTeamTableResultCommand command, int tournamentId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("Tables", "Tournaments", new { Id = tournamentId });
        }

        [HttpPost]
        public async Task<IActionResult> Activate(ActivateTeamTableResultCommand command, int tournamentId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("Tables", "Tournaments", new { Id = tournamentId });
        }

        public IActionResult AddPenaltyPoints(int teamTableResultId)
        {
            var viewModel = new AddPenaltyPointsCommand { TeamTableResultId = teamTableResultId };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPenaltyPoints(AddPenaltyPointsCommand command, int tournamentId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(AddPenaltyPoints), new { command.TeamTableResultId });
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction("Tables", "Tournaments", new { Id = tournamentId });
        }
    }
}

namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TeamTableResults.Commands.Activate;
    using BESL.Application.TeamTableResults.Commands.Drop;

    public class TableResultsController : AdminController
    {
        [HttpPost]
        public async Task<IActionResult> Drop(DropTeamTableResultCommand command, string tournamentId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("Tables", "Tournaments", new { Id = tournamentId });
        }

        [HttpPost]
        public async Task<IActionResult> Activate(ActivateTeamTableResultCommand command, string tournamentId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("Tables", "Tournaments", new { Id = tournamentId });
        }
    }
}

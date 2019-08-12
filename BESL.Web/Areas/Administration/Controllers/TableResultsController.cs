namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.TeamTableResults.Commands.Activate;
    using BESL.Application.TeamTableResults.Commands.Drop;

    public class TableResultsController : AdminController
    {
        [HttpPost]
        public async Task<IActionResult> Drop(DropTeamTableResultCommand command, string returnUrl)
        {
            await this.Mediator.Send(command);
            return this.LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(ActivateTeamTableResultCommand command, string returnUrl)
        {
            await this.Mediator.Send(command);
            return this.LocalRedirect(returnUrl);
        }
    }
}

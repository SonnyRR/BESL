namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Matches.Queries.Details;
    using BESL.Application.Matches.Queries.EditMatchResult;
    using BESL.Application.Matches.Commands.EditMatchResult;

    [Authorize]
    public class MatchesController : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Details(GetMatchDetailsQuery query)
            => this.View(await this.Mediator.Send(query));

        public async Task<IActionResult> EditResult(EditMatchResultQuery query)
            => this.View(await this.Mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> EditResult(EditMatchResultCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchDetailsQuery { Id = command.Id });
        }
    }
}

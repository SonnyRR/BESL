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
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        public async Task<IActionResult> EditResult(EditMatchResultQuery query)
        {
            var result = await this.Mediator.Send(query);
            return this.View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditResult(EditMatchResultCommand command)
        {
            var result = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchDetailsQuery { Id = command.Id });
        }
    }
}

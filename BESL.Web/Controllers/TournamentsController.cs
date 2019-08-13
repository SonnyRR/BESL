namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Commands.Enroll;
    using BESL.Application.Tournaments.Queries.Details;
    using BESL.Application.Tournaments.Queries.Enroll;

    [Authorize]
    public class TournamentsController : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Details(GetTournamentDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Enroll(EnrollATeamQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollATeamCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Details), new GetTournamentDetailsQuery { Id = command.TournamentId });
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetTournamentDetailsQuery { Id = command.TournamentId });
        }
    }
}

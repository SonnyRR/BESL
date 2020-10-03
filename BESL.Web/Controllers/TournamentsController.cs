namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Tournaments.Commands.Enroll;
    using BESL.Application.Tournaments.Queries.Details;
    using BESL.Application.Tournaments.Queries.Enroll;
    using BESL.Application.PlayWeeks.Queries.GetPlayWeeksForTournamentTable;

    [Authorize]
    public class TournamentsController : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Details(GetTournamentDetailsQuery query)
            => this.View(await this.Mediator.Send(query));

        public async Task<IActionResult> Enroll(EnrollATeamQuery query)
            => this.View(await this.Mediator.Send(query));      

        [HttpPost]
        public async Task<IActionResult> Enroll(EnrollATeamCommand command)
        {
            if (this.ModelState.IsValid)
            {
                await this.Mediator.Send(command);
            }

            return this.RedirectToAction(nameof(Details), new GetTournamentDetailsQuery { Id = command.TournamentId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> PlayWeeks(GetPlayWeeksForTournamentTableQuery query)
            => this.View(await this.Mediator.Send(query));
    }
}

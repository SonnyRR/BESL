namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Tournaments.Queries.Details;
    using BESL.Application.Tournaments.Queries.Enroll;
    using BESL.Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;

    public class TournamentsController : BaseController
    {
        private readonly IUserAcessor userAcessor;

        public TournamentsController(IUserAcessor userAcessor)
        {
            this.userAcessor = userAcessor;
        }

        public async Task<IActionResult> Details(GetTournamentDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Enroll(int tournamentId)
        {
            var viewModel = await this.Mediator.Send(new EnrollATeamQuery { TournamentId = tournamentId, UserId = this.userAcessor.UserId });
            return this.View(viewModel);
        }
    }
}

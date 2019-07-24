namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Tournaments.Queries.GetTournamentDetails;

    public class TournamentsController : BaseController
    {
        public async Task<IActionResult> Details(GetTournamentDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }
    }
}

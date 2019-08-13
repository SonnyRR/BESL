namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.Details;

    public class GamesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }   

        public async Task<IActionResult> All()
        {
            var viewModel = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(GetGameDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }
    }
}

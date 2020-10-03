namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.Details;

    public class GamesController : BaseController
    {
        public IActionResult Index()
            => this.View();

        public async Task<IActionResult> All()
            => this.View(await this.Mediator.Send(new GetAllGamesQuery()));

        public async Task<IActionResult> Details(GetGameDetailsQuery query)
            => this.View(await this.Mediator.Send(query));
    }
}

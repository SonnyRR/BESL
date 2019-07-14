namespace BESL.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BESL.Application.Players.Queries.PlayerDetails;
    using Microsoft.AspNetCore.Mvc;

    public class PlayersController : BaseController
    {
        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.Mediator.Send(new PlayerDetailsQuery() { Username = id });
            return this.View(viewModel);
        }
    }
}

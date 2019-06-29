namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Games.Queries.GetAllGames;

    public class FormatsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }
    }
}

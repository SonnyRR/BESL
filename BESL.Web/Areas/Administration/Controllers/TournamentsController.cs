namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BESL.Application.Games.Queries.GetAllGames;
    using Microsoft.AspNetCore.Mvc;

    public class TournamentsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
           return this.View();
        }

        public async Task<IActionResult> All()
        {
            return this.View(new GamesListViewModel() { Games = new List<GameLookupModel>()});
        }
    }
}

﻿namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;
    using BESL.Application.Games.Queries.GetAllGames;
    using BESL.Application.Games.Queries.GetGameDetails;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : BaseController
    {

        public IActionResult Index()
        {
            return this.View();
        }   

        public async Task<IActionResult> All()
        {
            var model = await this.Mediator.Send(new GetAllGamesQuery());
            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.Mediator.Send(new GameDetailsQuery() { Id = id });
            return this.View(model);
        }
    }
}
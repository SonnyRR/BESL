namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;
    using Microsoft.AspNetCore.Mvc;

    public class MatchFixturesController : AdminController
    {
        public async Task<IActionResult> Details(GetMatchesForPlayWeekQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(GetMatchesForPlayWeekQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }
    }
}

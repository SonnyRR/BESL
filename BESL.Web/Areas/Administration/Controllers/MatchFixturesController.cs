namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BESL.Application.Matches.Commands.Create;
    using BESL.Application.Matches.Queries.Create;
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;
    using Microsoft.AspNetCore.Mvc;

    public class MatchFixturesController : AdminController
    {
        public async Task<IActionResult> Details(GetMatchesForPlayWeekQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                var notValidViewModel = await this.Mediator.Send(new GetMatchesForPlayWeekQuery { PlayWeekId = command.PlayWeekId });
                return this.View("~/Areas/Administration/Views/MatchFixtures/Details.cshtml", notValidViewModel);
            }

            var viewModel = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchesForPlayWeekQuery { PlayWeekId = command.PlayWeekId });
        }
    }
}

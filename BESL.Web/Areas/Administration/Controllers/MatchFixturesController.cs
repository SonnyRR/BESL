﻿namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Matches.Commands.Create;
    using BESL.Application.Matches.Commands.Modify;
    using BESL.Application.Matches.Commands.Delete;
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;
    using BESL.Application.Matches.Queries.Modify;
    using BESL.Application.Matches.Commands.AcceptResult;

    public class MatchFixturesController : AdminController
    {
        public async Task<IActionResult> Details(GetMatchesForPlayWeekQuery query)
            => this.View(await this.Mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(
                    "~/Areas/Administration/Views/MatchFixtures/Details.cshtml", 
                    await this.Mediator.Send(new GetMatchesForPlayWeekQuery { PlayWeekId = command.PlayWeekId }));
            }
            
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchesForPlayWeekQuery { PlayWeekId = command.PlayWeekId });
        }

        public async Task<IActionResult> Modify(ModifyMatchQuery query)
            => this.View(await this.Mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Modify(ModifyMatchCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchesForPlayWeekQuery { PlayWeekId = command.PlayWeekId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMatchCommand command, int playWeekId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchesForPlayWeekQuery { PlayWeekId = playWeekId });
        }

        [HttpPost]
        public async Task<IActionResult> AcceptResult(AcceptMatchResultCommand command, int playWeekId)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(Details), new GetMatchesForPlayWeekQuery { PlayWeekId = playWeekId });
        }
    }
}

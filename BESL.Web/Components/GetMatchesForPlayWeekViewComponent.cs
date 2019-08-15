﻿namespace BESL.Web.Components
{
    using System.Threading.Tasks;
    using BESL.Application.Matches.Queries.GetMatchesForPlayWeek;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class GetMatchesForPlayWeekViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public GetMatchesForPlayWeekViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int playWeekId)
        {
            var viewModel = await this.mediator.Send(new GetMatchesForPlayWeekQuery { PlayWeekId = playWeekId });
            return this.View(viewModel);
        }
    }
}

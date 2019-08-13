namespace BESL.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Matches.Queries.Details;

    [Authorize]
    public class MatchesController : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Details(GetMatchDetailsQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }
    }
}

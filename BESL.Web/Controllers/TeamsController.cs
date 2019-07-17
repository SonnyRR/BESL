namespace BESL.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BESL.Application.Teams.Queries.Create;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var viewModel = await this.Mediator.Send(new CreateTeamQuery());
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string placeholder)
        {
            return this.View();
        }

        public async Task<IActionResult> Index(string id)
        {
            return this.View();
        }

        public async Task<IActionResult> Details(int id)
        {
            return this.View();
        }
    }
}

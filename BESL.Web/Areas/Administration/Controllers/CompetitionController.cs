namespace BESL.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using BESL.Application.Tournaments.Commands;

    public class CompetitionController : AdminController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateTournamentCommand command)
        {
            return this.View();
        }
    }
}

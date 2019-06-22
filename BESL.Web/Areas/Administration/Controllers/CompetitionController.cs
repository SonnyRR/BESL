namespace BESL.Web.Areas.Administration.Controllers
{
    using BESL.Application.Tournaments.Commands;
    using Microsoft.AspNetCore.Mvc;

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

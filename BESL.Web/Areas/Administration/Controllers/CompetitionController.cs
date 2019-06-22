namespace BESL.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CompetitionController : AdminController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return this.View();
        }
    }
}

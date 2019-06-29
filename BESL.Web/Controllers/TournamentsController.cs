namespace BESL.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TournamentsController : BaseController
    {
        public IActionResult Details(int id)
        {
            return this.View();
        }
    }
}

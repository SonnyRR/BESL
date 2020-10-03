namespace BESL.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Web.Models;

    public class HomeController : BaseController
    {
        public IActionResult Index()
            => this.View();
        
        public IActionResult Privacy()
            => this.View();

        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

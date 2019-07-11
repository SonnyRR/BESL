namespace BESL.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Web.Models;
    using BESL.Common.SteamWebApi;
    using Microsoft.Extensions.Configuration;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {    
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

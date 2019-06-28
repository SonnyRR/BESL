namespace BESL.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class TournamentsController : AdminController
    {
        public async Task<IActionResult> Create()
        {
           return this.View();
        }
    }
}

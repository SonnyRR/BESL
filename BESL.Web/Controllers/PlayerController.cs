namespace BESL.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class PlayerController : BaseController
    {
        public async Task<IActionResult> Details(string id)
        {
            return this.View();
        }
    }
}

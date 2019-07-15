namespace BESL.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return this.View();
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

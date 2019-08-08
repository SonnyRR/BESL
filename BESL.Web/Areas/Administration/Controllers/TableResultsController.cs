namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class TableResultsController : AdminController
    {
        [HttpPost]
        public async Task<IActionResult> Drop(int Id)
        {
            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Activate(int Id)
        {
            return this.Ok();
        }
    }
}

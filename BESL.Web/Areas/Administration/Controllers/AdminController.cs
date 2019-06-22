namespace BESL.Web.Areas.Administration.Controllers
{
    using BESL.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
    }
}

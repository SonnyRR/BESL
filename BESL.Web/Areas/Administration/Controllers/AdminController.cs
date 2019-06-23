namespace BESL.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using BESL.Web.Controllers;

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
    }
}

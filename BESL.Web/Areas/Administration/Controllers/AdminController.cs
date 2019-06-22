namespace BESL.Web.Areas.Administration.Controllers
{
    using BESL.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
    }
}

namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Web.Controllers;
    using BESL.Application.Interfaces;

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        private INotifyService userNotifyService;

        protected string UserNameIdentifier => this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}

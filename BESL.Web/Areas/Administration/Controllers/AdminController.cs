namespace BESL.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    using BESL.Web.Controllers;
    using BESL.Web.Services;

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        private INotifyService notifyService;

        protected INotifyService NotifyService
            => this.notifyService 
            ?? (this.notifyService = HttpContext.RequestServices.GetService<INotifyService>());

        protected string UserNameIdentifier => this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}

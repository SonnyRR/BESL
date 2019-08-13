namespace BESL.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using BESL.Application.Notifications.Commands.Delete;
    using BESL.Web.Filters;

    [Authorize]
    [AjaxOnlyFilter]
    public class NotificationsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteNotificationCommand command)
        {
            await this.Mediator.Send(command);
            return this.Ok();
        }
    }
}
namespace BESL.Web.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator 
            => mediator 
            ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
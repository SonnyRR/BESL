﻿namespace BESL.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdminController
    {
        public IActionResult Index() => this.RedirectToAction("Index", "Games");
    }
}

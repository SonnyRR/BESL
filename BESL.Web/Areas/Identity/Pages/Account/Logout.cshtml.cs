namespace BESL.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using BESL.Entities;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Player> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(SignInManager<Player> signInManager, ILogger<LogoutModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}
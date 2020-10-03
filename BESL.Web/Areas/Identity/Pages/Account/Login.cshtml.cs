namespace BESL.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using BESL.Entities;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(UserManager<Player> userManager, SignInManager<Player> signInManager, ILogger<LoginModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get => signInManager.GetExternalAuthenticationSchemesAsync().GetAwaiter().GetResult().ToList(); }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name ="Username or email")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {        
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            Player user = Input.Username
                .Contains('@')
                ? await this.userManager.FindByEmailAsync(Input.Username)
                : await this.userManager.FindByNameAsync(Input.Username);

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    if (user.IsDeleted)
                    {
                        ModelState.AddModelError(string.Empty, "This account is deleted, contact an administrator for restore!");
                        return Page();
                    }
                    
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username/email or password!");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

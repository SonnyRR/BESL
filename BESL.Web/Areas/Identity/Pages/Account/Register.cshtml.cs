namespace BESL.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using BESL.Entities;
    using static BESL.SharedKernel.GlobalConstants;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Player> signInManager;
        private readonly UserManager<Player> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<Player> userManager,
            SignInManager<Player> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (this.User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                var user = new Player { UserName = Input.Username, Email = Input.Email };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(PROFILE_AVATAR_CLAIM_TYPE, DEFAULT_AVATAR));
                    await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, DEFAULT_AVATAR_MEDIUM));
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

﻿namespace BESL.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using BESL.Application.Interfaces;
    using BESL.Entities;
    using BESL.Infrastructure.SteamWebAPI2;
    using static BESL.SharedKernel.GlobalConstants;

    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly IDeletableEntityRepository<Player> playersRepository;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Player> signInManager;
        private readonly UserManager<Player> userManager;
        private readonly ILogger<ExternalLoginModel> logger;

        public ExternalLoginModel(
            IDeletableEntityRepository<Player> playersRepository,
            IConfiguration configuration,
            SignInManager<Player> signInManager,
            UserManager<Player> userManager,
            ILogger<ExternalLoginModel> logger)
        {
            this.playersRepository = playersRepository;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            public string Username { get; set; }
        }

        public IActionResult OnGetAsync() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
#region Custom
            var userLogin = this.playersRepository
                .AllAsNoTrackingWithDeleted()
                .Include(x => x.Logins)
                .SelectMany(x => x.Logins)
                .Where(x => x.ProviderKey == info.ProviderKey)
                .SingleOrDefault();

            if (userLogin != null)
            {
                var isPlayerDeleted = (await this.playersRepository
                    .GetByIdWithDeletedAsync(userLogin.UserId))
                    .IsDeleted;

                if (isPlayerDeleted)
                {
                    ErrorMessage = $"Account associated with this steam account is deleted. Contact an administrator for restoring.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl, ErrorMessage = ErrorMessage });
                }
            }
#endregion
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);

                return LocalRedirect(returnUrl);
            }
            
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                var steamId64 = ulong.Parse(info.ProviderKey.Split('/').Last());
                var steamUser = SteamApiHelper.GetSteamUserInstance(this.configuration);
                var playerResult = await steamUser.GetCommunityProfileAsync(steamId64);

                if (playerResult.IsVacBanned)
                {
                    ErrorMessage = $"VAC banned accounts cannot register!";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }

                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new Player { UserName = Input.Username, Email = Input.Email };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        if (info.ProviderDisplayName == STEAM_PROVIDER_NAME)
                        {
                            var steamId64 = ulong.Parse(info.ProviderKey.Split('/').Last());
                            var steamUser = SteamApiHelper.GetSteamUserInstance(this.configuration);
                            var playerResult = await steamUser.GetCommunityProfileAsync(steamId64);

                            await this.userManager.AddClaimAsync(user, new Claim(STEAM_ID_64_CLAIM_TYPE, steamId64.ToString()));
                            await this.userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_CLAIM_TYPE, playerResult.AvatarFull.ToString()));
                            await this.userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, playerResult.AvatarMedium.ToString()));
                        }

                        await signInManager.SignInAsync(user, isPersistent: true);

                        logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}

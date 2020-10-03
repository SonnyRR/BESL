﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BESL.Entities;
using BESL.Infrastructure.SteamWebAPI2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using static BESL.Common.GlobalConstants;

namespace BESL.Web.Areas.Identity.Pages.Account.Manage
{
    public class ExternalLoginsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Player> _userManager;
        private readonly SignInManager<Player> _signInManager;

        public ExternalLoginsModel(
            IConfiguration configuration,
            UserManager<Player> userManager,
            SignInManager<Player> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            CurrentLogins = await _userManager.GetLoginsAsync(user);
            OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            ShowRemoveButton = user.PasswordHash != null || CurrentLogins.Count > 1;
            return Page();
        }

        //public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
        //    if (!result.Succeeded)
        //    {
        //        var userId = await _userManager.GetUserIdAsync(user);
        //        throw new InvalidOperationException($"Unexpected error occurred removing external login for user with ID '{userId}'.");
        //    }

        //    await _signInManager.RefreshSignInAsync(user);

        //    StatusMessage = "The external login was removed.";

        //    var playerClaims = await this._userManager.GetClaimsAsync(user);
        //    await this._userManager.RemoveClaimsAsync(user, playerClaims);

        //    await this._userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_CLAIM_TYPE, DEFAULT_AVATAR));
        //    await this._userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, DEFAULT_AVATAR_MEDIUM));

        //    return RedirectToPage();
        //}

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";

            if (info.ProviderDisplayName == STEAM_PROVIDER_NAME)
            {
                var steamId64 = ulong.Parse(info.ProviderKey.Split('/').Last());
                var steamUser = SteamApiHelper.GetSteamUserInstance(this._configuration);
                var playerResult = await steamUser.GetCommunityProfileAsync(steamId64);

                var userClaims = await this._userManager.GetClaimsAsync(user);

                await this._userManager.AddClaimAsync(user, new Claim(STEAM_ID_64_CLAIM_TYPE, steamId64.ToString()));

                await this._userManager.RemoveClaimAsync(user, new Claim(PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, DEFAULT_AVATAR_MEDIUM));
                await this._userManager.RemoveClaimAsync(user, new Claim(PROFILE_AVATAR_CLAIM_TYPE, DEFAULT_AVATAR));

                await this._userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_CLAIM_TYPE, playerResult.AvatarFull.ToString()));
                await this._userManager.AddClaimAsync(user, new Claim(PROFILE_AVATAR_MEDIUM_CLAIM_TYPE, playerResult.AvatarMedium.ToString()));
            }
            return RedirectToPage();
        }
    }
}

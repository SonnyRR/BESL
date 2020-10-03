namespace BESL.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    
    using BESL.Entities;

    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<Player> userManager;

        public ConfirmEmailModel(UserManager<Player> userManager) => this.userManager = userManager;

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }
    }
}

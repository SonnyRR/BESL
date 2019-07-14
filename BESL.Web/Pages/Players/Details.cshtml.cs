namespace BESL.Web.Pages
{
    using System.Threading;
    using System.Linq;
    using System.Threading.Tasks;
    using BESL.Domain.Entities;
    using static BESL.Common.GlobalConstants;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class DetailsModel : PageModel
    {
        private readonly UserManager<Player> userManager;
        private readonly IUserStore<Player> userStore;

        public DetailsModel(UserManager<Player> userManager, IUserStore<Player> userStore)
        {
            this.userManager = userManager;
            this.userStore = userStore;
        }

        public string Name { get; set; }

        public bool IsVacBanned { get; set; }

        public string AvatarFullUrl { get; set; }

        public string SteamAccRegisterDate { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var desiredUser = await this.userStore.FindByNameAsync(id.ToUpper(), CancellationToken.None);
            if (desiredUser != null)
            {
                var claims = await this.userManager.GetClaimsAsync(desiredUser);
                this.Name = desiredUser.UserName;
                this.IsVacBanned = claims.Any(c=>c.Type == IS_VAC_BANNED_CLAIM_TYPE);
                this.AvatarFullUrl = claims.SingleOrDefault(c => c.Type == PROFILE_AVATAR_CLAIM_TYPE).Value;

            }
            return Page();
        }
    }
}

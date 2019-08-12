namespace BESL.Infrastructure
{
    using System;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Http;

    using BESL.Application.Interfaces;

    public class UserAccessor : IUserAcessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public ClaimsPrincipal User => this.httpContextAccessor.HttpContext?.User;

        public string UserId => this.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Annonymous";

        public string Username => this.User?.FindFirstValue(ClaimTypes.Name) ?? "John Doe";
    }
}

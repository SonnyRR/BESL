namespace BESL.Application.Interfaces
{
    using System.Security.Claims;

    public interface IUserAcessor
    {
        ClaimsPrincipal User { get; }

        string UserId { get; }
    }
}

namespace BESL.Application.Exceptions
{
    public class PlayerAlreadyHasPendingInvite : BaseCustomException
    {
        public PlayerAlreadyHasPendingInvite(string userName)
            : base($"{userName} already has a pending invite from this team!")
        {
        }
    }
}

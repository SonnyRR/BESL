namespace BESL.Application.Exceptions
{
    public class PlayerDoesNotHaveALinkedSteamAccountException : BaseCustomException
    {
        public PlayerDoesNotHaveALinkedSteamAccountException()
            : base("You don't have a linked steam account to complete this action.")
        {
        }
    }
}

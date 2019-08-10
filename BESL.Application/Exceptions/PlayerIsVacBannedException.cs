namespace BESL.Application.Exceptions
{
    public class PlayerIsVacBannedException : BaseCustomException
    {
        public PlayerIsVacBannedException()
            : base("You are VAC banned!")
        {
        }

        public PlayerIsVacBannedException(string username)
            : base($"{username} is VAC banned!")
        {
        }
    }
}

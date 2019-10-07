namespace BESL.Application.Exceptions
{
    public class PlayerDoesNotExistException : BaseCustomException
    {
        public PlayerDoesNotExistException(string username)
            : base($"Player with username: {username} does not exist!")
        {
        }
    }
}

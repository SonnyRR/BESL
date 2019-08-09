namespace BESL.Application.Exceptions
{
    using static BESL.Common.GlobalConstants;

    public class PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException : BaseCustomException
    {

        public PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException()
            : base("You are already a member of a team with the same format!")
        {
        }

        public PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException(string username)
            : base($"{username} is already a member of a team with the same format!")
        {
        }
    }
}

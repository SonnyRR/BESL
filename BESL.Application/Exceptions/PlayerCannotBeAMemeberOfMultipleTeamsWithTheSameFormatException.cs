namespace BESL.Application.Exceptions
{
    using static BESL.Common.GlobalConstants;

    public class PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException : BaseCustomException
    {
        public PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException(string username) 
            : base($"{username} is already a memeber of a team with the same format!")
        {
        }

        public PlayerCannotBeAMemeberOfMultipleTeamsWithTheSameFormatException()
            : base(PLAYER_ALREADY_PART_OF_A_TEAM_MSG)
        {
        }
    }
}

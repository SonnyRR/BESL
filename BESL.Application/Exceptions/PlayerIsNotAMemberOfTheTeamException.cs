namespace BESL.Application.Exceptions
{
    public class PlayerIsNotAMemberOfTheTeamException : BaseCustomException
    {
        public PlayerIsNotAMemberOfTheTeamException()
            : base ("Player is not a member of the current team!")
        {
        }
    }
}

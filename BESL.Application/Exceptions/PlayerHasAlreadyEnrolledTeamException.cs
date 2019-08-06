namespace BESL.Application.Exceptions
{
    public class PlayerHasAlreadyEnrolledTeamException : BaseCustomException
    {
        public PlayerHasAlreadyEnrolledTeamException()
            : base("You have already enrolled a team with the same format in a tournament!")
        {
        }
    }
}

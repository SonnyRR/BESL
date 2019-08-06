namespace BESL.Application.Exceptions
{
    public class PlayerHasNoEligibleTeamsToEnrollException : BaseCustomException
    {
        public PlayerHasNoEligibleTeamsToEnrollException()
            : base("You don't have any eligable teams to enroll!")
        {
        }
    }
}

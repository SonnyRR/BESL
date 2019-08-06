namespace BESL.Application.Exceptions
{
    public class TournamentActiveDateMustStartOnMondayException : BaseCustomException
    {
        public TournamentActiveDateMustStartOnMondayException()
            : base ("Tournament active date must start on Monday!")
        {
        }
    }
}

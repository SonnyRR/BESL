namespace BESL.Application.Exceptions
{
    public class TeamFormatDoesNotMatchTournamentFormatException : BaseCustomException
    {
        public TeamFormatDoesNotMatchTournamentFormatException()
            : base("Team format does not match the desired tournament format!")
        {
        }
    }
}

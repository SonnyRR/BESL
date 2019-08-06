namespace BESL.Application.Exceptions
{
    public class TournamentTableIsFullException : BaseCustomException
    {
        public TournamentTableIsFullException()
            : base("Desired tournament skill table is full!")
        {
        }
    }
}

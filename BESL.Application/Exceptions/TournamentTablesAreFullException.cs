namespace BESL.Application.Exceptions
{
    public class TournamentTablesAreFullException : BaseCustomException
    {
        public TournamentTablesAreFullException()
            : base("Tournament skill tables are full!")
        {
        }
    }
}

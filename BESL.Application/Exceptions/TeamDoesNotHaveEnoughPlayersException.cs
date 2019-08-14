namespace BESL.Application.Exceptions
{
    public class TeamDoesNotHaveEnoughPlayersException : BaseCustomException
    {
        public TeamDoesNotHaveEnoughPlayersException(string teamName)
            : base($"{teamName} does not meet the minimum requirement for active players!")
        {
        }
    }
}

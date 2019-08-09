namespace BESL.Application.Exceptions
{
    public class TeamIsFullException : BaseCustomException
    {
        public TeamIsFullException(string teamName)
            : base($"{teamName} is full!")
        {
        }
    }
}

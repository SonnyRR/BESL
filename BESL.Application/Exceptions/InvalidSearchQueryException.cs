namespace BESL.Application.Exceptions
{
    public class InvalidSearchQueryException : BaseCustomException
    {
        public InvalidSearchQueryException()
            : base("Invalid search query!")
        {
        }
    }
}

namespace BESL.Application.Exceptions
{
    public class DeleteFailureException : BaseCustomException
    {
        public DeleteFailureException(string name, object key, string message)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
        }
    }
}

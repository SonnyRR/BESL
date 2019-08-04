namespace BESL.Application.Exceptions
{
    public class EntityAlreadyExistsException : BaseCustomException
    {
        public EntityAlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists or is marked as deleted!")
        {
        }
    }
}

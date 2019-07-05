namespace BESL.Application.Exceptions
{
    public class EntityAlreadyExists : BaseCustomException
    {
        public EntityAlreadyExists(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists or is marked as deleted!")
        {
        }
    }
}

namespace BESL.Persistence.Redis
{
    using BESL.Application.Interfaces;
    using BESL.Persistence.Infrastructure;

    public class RedisNotificationService<T> : BaseRedisService<T>, IRedisService<T>
    {
        public RedisNotificationService()
        {
        }

        public void Delete(string key)
        {
            throw new System.NotImplementedException();
        }

        public T Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string key, T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}

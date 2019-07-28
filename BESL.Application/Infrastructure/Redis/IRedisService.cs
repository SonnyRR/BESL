namespace BESL.Application.Infrastructure.Redis
{
    public interface IRedisService<T>
    {
        T Get(string key);

        void Save(string key, T obj);

        void Delete(string key); 
    }
}
namespace BESL.Application.Interfaces
{
    using System;

    public interface IRedisService<T>
    {
        T Get(string key);

        void Save(string key, T obj, TimeSpan? expiration = null);

        void Delete(string key); 
    }
}
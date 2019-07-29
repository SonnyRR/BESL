namespace BESL.Persistence.Infrastructure
{
    using System;

    using StackExchange.Redis;

    using BESL.Common;
    using BESL.Application.Interfaces;

    public class RedisService<T> : IRedisService<T>
    {
        public IDatabase Database { get; private set; }

        public RedisService(IRedisConnectionFactory redisConnectionFactory)
        {
            this.Database = redisConnectionFactory.Connection().GetDatabase();
        }

        public void Save(string key, T data)
        {
            var serializedObject = JsonHelper.SerializeObjectToJson(data);
            this.Database.StringSet(key, serializedObject);
        }

        public T Get(string key)
        {
            try
            {
                var res = this.Database.StringGet(key);

                if (res.IsNull)
                    return default;

                return JsonHelper.DeserializeObjectFromJson<T>(res);
            }
            catch
            {
                return default;
            }
        }

        public void Delete(string key)
        {
            if(string.IsNullOrWhiteSpace(key) || key.Contains(":"))
                throw new ArgumentException("invalid key");

            this.Database.KeyDelete(key);
        }
    }
}
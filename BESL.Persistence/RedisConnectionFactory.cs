namespace BESL.Persistence
{
    using System;

    using Microsoft.Extensions.Options;
    using StackExchange.Redis;

    using BESL.Persistence.Infrastructure;

    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;       

        private readonly IOptions<RedisConfiguration> redis;

        public RedisConnectionFactory(IOptions<RedisConfiguration> redis)
        {
            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redis.Value.Host));
        }

        public ConnectionMultiplexer Connection()
        {
            return this._connection.Value;
        }
    }
}
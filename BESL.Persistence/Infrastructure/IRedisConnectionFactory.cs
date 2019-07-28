namespace BESL.Persistence.Infrastructure
{ 
    using StackExchange.Redis;

    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
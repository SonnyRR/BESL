namespace BESL.Application.Infrastructure.Redis
{
    using StackExchange.Redis;

    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
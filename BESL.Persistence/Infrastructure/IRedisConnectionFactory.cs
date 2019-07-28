namespace BESL.Application.Interfaces
{ 
    using StackExchange.Redis;

    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
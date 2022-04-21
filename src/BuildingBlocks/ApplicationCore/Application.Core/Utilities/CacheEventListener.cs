using StackExchange.Redis;

namespace Application.Core.Utilities;

public class CacheEventListener
{
    private readonly IConnectionMultiplexer _multiplexer;
    private const string ChatChannel = "__keyspace@0__:";
    
    public CacheEventListener(IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
    }

    public async Task ListenExpiredByPrefix(string prefix)
    {
        // await _multiplexer.GetServer(_multiplexer.GetEndPoints().Single())
        //     .ConfigSetAsync("notify-keyspace-events", "Kx");
        await _multiplexer.GetServer(_multiplexer.GetEndPoints().Single())
            .ConfigSetAsync("notify-keyspace-events", "KEA");
        await _multiplexer.GetSubscriber().SubscribeAsync(ChatChannel + prefix + "*", Handler
        );
    }

    private async void Handler(RedisChannel channel, RedisValue redisValue)
    {
       Console.WriteLine($"received product key {redisValue} {channel.ToString()}");
    }
}
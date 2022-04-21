using Application.Core.Caching;
using Application.Core.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Application.Core.Extensions;

public static class RedisDependencyInjectionExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services,string host)
    {
        var muxer = ConnectionMultiplexer.Connect(host+",allowAdmin=true");
        services.AddSingleton<IConnectionMultiplexer>(muxer);
        services.AddSingleton<IServer>(o=> muxer.GetServer(host));
        services.AddSingleton<IDatabase>(o => muxer.GetDatabase());
        services.AddSingleton<ICacheManager, RedisCacheManager>();
        return services;
    }
}

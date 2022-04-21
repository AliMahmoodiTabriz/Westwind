// See https://aka.ms/new-console-template for more information

using Application.Core.Utilities;
using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("172.16.0.207:6379,allowAdmin=true");

var cache = new CacheEventListener(muxer);
cache.InformationDataReady += (channel,redisValue) =>
{
    Console.WriteLine($"received product key {redisValue} {channel.ToString()}");
};

Console.ReadKey();


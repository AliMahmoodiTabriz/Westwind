using Newtonsoft.Json;
using StackExchange.Redis;

namespace Application.Core.Caching.Redis;

public class RedisCacheManager : ICacheManager
{
    private readonly IDatabase _database;
    private readonly IServer _server;

    public RedisCacheManager(IDatabase database, IServer server)
    {
        _database = database;
        _server = server;
    }

    public async Task Add(string key, object data, int duration = 15)
    {
        await _database.StringSetAsync(key, JsonConvert.SerializeObject(data), new TimeSpan(0, 0, 0, duration));
    }

    public async Task AddPersistent(string key, object data)
    {
        await _database.StringSetAsync(key, JsonConvert.SerializeObject(data));
    }

    public async Task<T> Get<T>(string key)
    {
        var serializedObject = await _database.StringGetAsync(key);
        if (serializedObject.IsNullOrEmpty)
            return await Task.FromResult<T>(default);

        return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
    }

    public async Task<List<T>> GetAll<T>(string pattern="")
    {
        List<T> listObject = new List<T>();
        var keys = _server.KeysAsync(_database.Database, pattern + "*").GetAsyncEnumerator();
        while (await keys.MoveNextAsync())
        {
            var serializedObject = await _database.StringGetAsync(keys.Current.ToString());
            listObject.Add(JsonConvert.DeserializeObject<T>(serializedObject.ToString()));
        }

        return listObject;
    }

    public async Task<bool> Any(string pattern)
    {
        return _server.Keys(_database.Database, pattern + "*").Any();
    }

    public async IAsyncEnumerable<string> GetKeys(string pattern)
    {
        var result = _server.KeysAsync(_database.Database, pattern + "*").GetAsyncEnumerator();
        while (await result.MoveNextAsync())
            yield return result.Current.ToString();
    }

    public async Task<bool> IsAdd(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task Remove(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task RemoveByPattern(string pattern)
    {
        var keys = _server.Keys(_database.Database, pattern + "*");
        await _database.KeyDeleteAsync(keys.ToArray());
    }
}
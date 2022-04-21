namespace Application.Core.Caching;

public interface ICacheManager
{
    Task<T> Get<T>(string key);
    Task<List<T>> GetAll<T>(string pattern="");
    IAsyncEnumerable<string> GetKeys(string pattern);
    Task Add(string key, object data, int duration=20);
    Task AddPersistent(string key, object data);
    Task<bool> IsAdd(string key);
    Task Remove(string key);
    Task RemoveByPattern(string pattern);
    Task<bool> Any(string pattern);
}

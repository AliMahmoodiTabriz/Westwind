using Application.Core.Caching;
using RedisCache.Application.Abstract;
using RedisCache.Domian;

namespace RedisCache.Application.Concrete;

public class ProductManager : IProductService
{
    private readonly ICacheManager _cache;

    public ProductManager(ICacheManager cache)
    {
        _cache = cache;
    }

    public async Task<Product> Add(Product product)
    {
        await _cache.AddPersistent(product.Key, product);
        return product;
    }

    public async Task RemoveAll()
    {
        var keys = _cache.GetKeys("").GetAsyncEnumerator();
        while (await keys.MoveNextAsync())
        {
            await _cache.Remove(keys.Current);
        }
    }

    public async Task<IList<Product>> GetAll()
    {
        return await _cache.GetAll<Product>();
    }

    public async Task<IList<Product>> GetByCategory(string category)
    {
        return await _cache.GetAll<Product>(category);
    }

    public async Task<Product> GetById(string id)
    {
        return (await _cache.GetAll<Product>("*"+id)).FirstOrDefault();
    }

    public async Task<Product> GetByName(string name)
    {
        return (await _cache.GetAll<Product>("*"+name)).FirstOrDefault();
    }
}
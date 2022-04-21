using RedisCache.Domian;

namespace RedisCache.Application.Abstract;

public interface IProductService
{
    Task<Product> Add(Product product);
    Task RemoveAll();
    Task<IList<Product>> GetAll();
    Task<IList<Product>>  GetByCategory(string category);
    Task<Product> GetById(string id);
    Task<Product> GetByName(string name);
    
}
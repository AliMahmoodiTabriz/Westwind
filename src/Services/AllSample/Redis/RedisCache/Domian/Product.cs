namespace RedisCache.Domian;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal UnitPrice { get; set; }

    public string Key => Category + Name + Id;
}
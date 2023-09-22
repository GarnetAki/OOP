namespace Shops.Models;

public class Product
{
    public Product(string name, Guid id)
    {
        Name = name;
        Id = id;
    }

    public string Name { get; }
    public Guid Id { get; }
}
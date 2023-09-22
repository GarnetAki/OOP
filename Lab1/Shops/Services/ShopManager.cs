using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopManager
{
    private readonly List<Product> _products;
    private readonly List<Shop> _shops;

    public ShopManager()
    {
        _shops = new List<Shop>();
        _products = new List<Product>();
    }

    public IReadOnlyList<Product> Products => _products;

    public IReadOnlyList<Shop> Shops => _shops;

    public Shop AddShop(string shopName, string shopAddress)
    {
        var newShop = new Shop(new ShopName(shopName), Guid.NewGuid(), new ShopAddress(shopAddress));
        _shops.Add(newShop);
        return newShop;
    }

    public void BuyProductsInShop(Person person, Shop shop)
    {
        shop.SellProducts(person.ShoppingList, person);
    }

    public void BuyProductsCheapestWay(Person person)
    {
        FindCheapest(person.ShoppingList).SellProducts(person.ShoppingList, person);
    }

    public void AddProduct(Product newProduct)
    {
        if (_products.All(product => product.Id != newProduct.Id)) _products.Add(newProduct);
    }

    public void ShopReplenishment(Shop shop, List<ProductPosition> productList)
    {
        foreach (ProductPosition productPosition in productList)
            AddProduct(productPosition.Product);
        shop.ProductsReplenishment(productList);
    }

    private Shop FindCheapest(IReadOnlyList<ShoppingListPosition> productsList)
    {
        if (_shops.All(shop => !shop.CheckProductsAvailability(productsList)))
            throw ShopManagerException.CreateCanNotFindShop();
        return _shops.FindAll(shop => shop.CheckProductsAvailability(productsList)).OrderBy(shop => shop.PriceOfProducts(productsList)).First();
    }
}
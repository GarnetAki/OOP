using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTest
{
    [Fact]
    public void ReplenishmentToShop_PersonCanBuyThem()
    {
        var shopManager = new ShopManager();
        shopManager.AddShop("Real", "Stoneisland 22");
        var products = new List<ProductPosition>();
        var newPerson = new Person(new PersonName("V V V"), 2000);
        var vodaArhiz = new Product("Voda Arhiz", Guid.NewGuid());
        var vodaSl = new Product("Voda Shishkin Les", Guid.NewGuid());

        products.Add(new ProductPosition(vodaArhiz, 50, 87));
        products.Add(new ProductPosition(vodaSl, 30, 77));

        shopManager.ShopReplenishment(shopManager.Shops[0], products);
        newPerson.AddPosition(shopManager.Products[0], 10);
        shopManager.BuyProductsInShop(newPerson, shopManager.Shops[0]);

        Assert.True(newPerson.Money == 2000 - (10 * 87));
        Assert.Equal(40, shopManager.Shops[0].ProductAmount(vodaArhiz));
    }

    [Fact]
    public void SetPrice_ChangePrice()
    {
        var shopManager = new ShopManager();
        shopManager.AddShop("Real", "Stoneisland 22");
        var product = new Product("Voda Arhiz", Guid.NewGuid());
        var products = new List<ProductPosition> { new (product, 50, 87) };

        shopManager.ShopReplenishment(shopManager.Shops[0], products);

        Assert.True(shopManager.Shops[0].ProductPrice(product) == 87);
        shopManager.Shops[0].ChangePrice(product, 89);
        Assert.True(shopManager.Shops[0].ProductPrice(product) == 89);
    }

    [Fact]
    public void PersonBuyListOfProductsInCheapestWay_ShopsHaveNotEnoughProducts_PersonHaveNotEnoughMoney()
    {
        var shopManager = new ShopManager();
        shopManager.AddShop("Real", "Stoneisland 22");
        shopManager.AddShop("UnReal", "Stoneisland 23");
        var products = new List<ProductPosition>();
        var productsSec = new List<ProductPosition>();
        var newPerson = new Person(new PersonName("V V V"), 2000);
        var vodaArhiz = new Product("Voda Arhiz", Guid.NewGuid());
        var vodaSl = new Product("Voda Shishkin Les", Guid.NewGuid());

        products.Add(new ProductPosition(vodaArhiz, 50, 87));
        products.Add(new ProductPosition(vodaSl, 30, 77));
        productsSec.Add(new ProductPosition(vodaArhiz, 20, 37));
        productsSec.Add(new ProductPosition(vodaSl, 40, 70));

        shopManager.ShopReplenishment(shopManager.Shops[0], products);
        shopManager.ShopReplenishment(shopManager.Shops[1], productsSec);
        newPerson.AddPosition(shopManager.Products[0], 10);
        newPerson.AddPosition(shopManager.Products[1], 15);
        shopManager.BuyProductsCheapestWay(newPerson);

        Assert.True(newPerson.Money == 2000 - ((10 * 37) + (15 * 70)));
        newPerson.AddPosition(shopManager.Products[0], 30);
        Assert.Throws<MoneyException>(() => shopManager.BuyProductsCheapestWay(newPerson));
        newPerson.AddPosition(shopManager.Products[0], 100);
        Assert.Throws<ShopManagerException>(() => shopManager.BuyProductsCheapestWay(newPerson));
    }

    [Fact]
    public void ReplenishmentToShop_PersonCanBuyThe()
    {
        var shopManager = new ShopManager();
        shopManager.AddShop("Real", "Stoneisland 22");
        var products = new List<ProductPosition>();
        var newPerson = new Person(new PersonName("V V V"), 2000);
        var vodaArhiz = new Product("Voda Arhiz", Guid.NewGuid());
        var vodaSl = new Product("Voda Shishkin Les", Guid.NewGuid());

        products.Add(new ProductPosition(vodaArhiz, 50, 87));
        products.Add(new ProductPosition(vodaSl, 30, 77));

        shopManager.ShopReplenishment(shopManager.Shops[0], products);
        newPerson.AddPosition(shopManager.Products[0], 10);
        newPerson.AddPosition(shopManager.Products[1], 5);

        Assert.True(shopManager.Shops[0].CheckProductsAvailability(newPerson.ShoppingList));
        Assert.True(shopManager.Shops[0].PriceOfProducts(newPerson.ShoppingList) <= newPerson.Money);
        shopManager.BuyProductsInShop(newPerson, shopManager.Shops[0]);
        Assert.Equal(newPerson.Money, 2000 - ((10 * 87) + (5 * 77)));
        Assert.Equal(40, shopManager.Shops[0].ProductAmount(vodaArhiz));
        Assert.Equal(25, shopManager.Shops[0].ProductAmount(vodaSl));
    }
}
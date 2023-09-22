using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManage
{
    Shop AddShop(string shopName, string shopAddress);

    Shop FindCheapest(List<ShoppingListPosition> productsList);

    void BuyProductsInShop(Person person, Shop shop);

    void BuyProductsCheapestWay(Person person);

    void ShopReplenishment(Shop shop, List<ProductPosition> productList);
}
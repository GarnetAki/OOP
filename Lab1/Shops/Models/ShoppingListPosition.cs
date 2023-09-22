using Shops.Exceptions;

namespace Shops.Models;

public class ShoppingListPosition
{
    public ShoppingListPosition(Product product, int amount)
    {
        CheckNonNegative(amount);
        Product = product;
        Amount = amount;
    }

    public Product Product { get; }
    public int Amount { get; private set; }

    public void Replenishment(int count)
    {
        CheckNonNegative(count);
        Amount += count;
    }

    private static void CheckNonNegative(int num)
    {
        if (num < 0)
            throw ProductPositionException.CreateWishNegativeAmount();
    }
}
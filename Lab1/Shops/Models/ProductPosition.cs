using Shops.Exceptions;

namespace Shops.Models;

public class ProductPosition
{
    public ProductPosition(Product product, int amount, decimal price)
    {
        CheckPriceIsValid(price);
        Product = product;
        Amount = amount;
        Price = price;
    }

    public Product Product { get; }
    public int Amount { get; private set; }
    public decimal Price { get; private set; }

    public bool CheckAvailable(int count)
    {
        return Amount - count >= 0;
    }

    public void Sell(int count)
    {
        CheckProductIsEnough(Amount - count);
        Amount -= count;
    }

    public void Replenishment(string newName, int count, decimal newPrice)
    {
        CheckPriceIsValid(newPrice);
        CheckReplenishmentIsValid(count);
        Amount += count;
        Price = newPrice;
    }

    private static void CheckProductIsEnough(int num)
    {
        if (num < 0)
            throw ProductPositionException.CreateProductNotEnough();
    }

    private static void CheckPriceIsValid(decimal num)
    {
        if (num < 0)
            throw ProductPositionException.CreateInvalidPrice();
    }

    private static void CheckReplenishmentIsValid(int num)
    {
        if (num < 0)
            throw ProductPositionException.CreateInvalidReplenishment();
    }
}
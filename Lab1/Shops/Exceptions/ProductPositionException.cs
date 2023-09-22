namespace Shops.Exceptions;

public class ProductPositionException : ShopException
{
    private ProductPositionException(string message)
        : base(message)
    {
    }

    public static ProductPositionException CreateProductNotEnough()
        => new ProductPositionException($"Shop haven't got enough products.");

    public static ProductPositionException CreateWishNegativeAmount()
        => new ProductPositionException($"Person can't wish negative amount of products.");

    public static ProductPositionException CreateInvalidPrice()
        => new ProductPositionException($"Price can't be negative.");

    public static ProductPositionException CreateInvalidReplenishment()
        => new ProductPositionException($"Replenishment can't be negative number.");
}
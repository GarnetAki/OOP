namespace Shops.Exceptions;

public class ShopManagerException : Exception
{
    private ShopManagerException(string message)
        : base(message)
    {
    }

    public static ShopManagerException CreateCanNotFindShop()
        => new ShopManagerException($"Shop with enough products doesn't exist.");
}
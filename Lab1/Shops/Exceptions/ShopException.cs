namespace Shops.Exceptions;

public abstract class ShopException : Exception
{
    protected ShopException(string message)
        : base(message)
    {
    }
}
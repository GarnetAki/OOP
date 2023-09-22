namespace Shops.Exceptions;

public class ShopAddressException : ShopException
{
    private ShopAddressException(string message)
        : base(message)
    {
    }

    public static ShopAddressException CreateInvalidFormat()
        => new ShopAddressException($"Address format is not correct.");

    public static ShopAddressException CreateInvalidChar()
        => new ShopAddressException($"Address characters are not correct.");
}
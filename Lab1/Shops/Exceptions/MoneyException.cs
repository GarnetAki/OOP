namespace Shops.Exceptions;

public class MoneyException : ShopException
{
    private MoneyException(string message)
        : base(message)
    {
    }

    public static MoneyException CreateNotEnoughMoney()
        => new MoneyException($"Person haven't got enough money.");
}
namespace Banks.Exceptions;

public class BankException : Exception
{
    private BankException(string message)
        : base(message)
    {
    }

    public static BankException CreateClientDoesNotExist()
        => new BankException($"Client does not exist.");

    public static BankException CreateClientAlreadyExists()
        => new BankException($"Client already exists.");

    public static BankException CreateNotEnoughMoney()
        => new BankException($"Money is not enough.");

    public static BankException CreateOperationExceedsLimit()
        => new BankException($"Operation exceeds limit.");
}
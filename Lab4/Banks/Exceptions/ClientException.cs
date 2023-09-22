namespace Banks.Exceptions;

public class ClientException : Exception
{
    private ClientException(string message)
        : base(message)
    {
    }

    public static ClientException CreateInvalidPassport()
        => new ClientException($"Passport is invalid.");

    public static ClientException CreateInvalidAddress()
        => new ClientException($"Address is invalid.");
}
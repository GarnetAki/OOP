namespace Isu.Extra.Exceptions;

public class AddressException : IsuServiceException
{
    private AddressException(string message)
        : base(message)
    {
    }

    public static AddressException CreateInvalidFormat()
        => new AddressException($"Address have invalid format.");

    public static AddressException CreateInvalidChar()
        => new AddressException($"Address have invalid char.");
}
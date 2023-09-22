namespace Shops.Exceptions;

public class PersonNameException : Exception
{
    private PersonNameException(string message)
        : base(message)
    {
    }

    public static PersonNameException CreateInvalidFormat()
        => new PersonNameException($"Name format is not correct.");

    public static PersonNameException CreateInvalidChar()
        => new PersonNameException($"Name characters are not correct.");
}
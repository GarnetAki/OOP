namespace Isu.Services;

public class NameIsNotCorrectException : StudentExceptions
{
    private NameIsNotCorrectException(string message)
        : base(message)
    {
    }

    public static void ThrowEmpty()
    {
        throw new NameIsNotCorrectException($"Name is empty.");
    }

    public static void ThrowFormat()
    {
        throw new NameIsNotCorrectException($"Name format is not correct.");
    }

    public static void ThrowChar()
    {
        throw new NameIsNotCorrectException($"Name characters are not correct.");
    }
}
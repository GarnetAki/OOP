namespace Backups.Extra.Exceptions;

public class ClockException : Exception
{
    private ClockException(string message)
        : base(message)
    {
    }

    public static ClockException RemoveException()
        => new ClockException($"Observer is already not in list.");

    public static ClockException AddException()
        => new ClockException($"Observer is already in list.");
}
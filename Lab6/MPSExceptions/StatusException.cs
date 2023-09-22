namespace MPSExceptions;

public class StatusException : Exception
{
    private StatusException(string message)
        : base(message)
    {
    }

    public static StatusException InvalidStatusException()
        => new StatusException($"Status must be next to previous.");
}
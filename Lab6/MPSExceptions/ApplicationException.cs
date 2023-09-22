namespace MPSExceptions;

public abstract class ApplicationException : Exception
{
    protected ApplicationException(string? message)
        : base(message) { }
}
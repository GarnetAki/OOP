namespace Isu.Services;

public abstract class IsuServiceException : Exception
{
    protected IsuServiceException(string message)
        : base(message)
    {
    }
}
namespace Isu.Extra.Exceptions;

public class IsuServiceException : Exception
{
    protected IsuServiceException(string message)
        : base(message)
    {
    }
}
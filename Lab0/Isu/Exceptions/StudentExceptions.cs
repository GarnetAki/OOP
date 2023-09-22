namespace Isu.Services;

public abstract class StudentExceptions : IsuServiceException
{
    protected StudentExceptions(string message)
        : base(message)
    {
    }
}
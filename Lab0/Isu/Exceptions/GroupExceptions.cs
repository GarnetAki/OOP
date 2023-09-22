namespace Isu.Services;

public abstract class GroupExceptions : IsuServiceException
{
    protected GroupExceptions(string message)
        : base(message)
    {
    }
}
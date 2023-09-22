namespace Backups.Exceptions;

public class RestorePointExceptions : Exception
{
    private RestorePointExceptions(string message)
        : base(message)
    {
    }

    public static RestorePointExceptions RemoveException()
        => new RestorePointExceptions($"Restore point already not in list.");
}
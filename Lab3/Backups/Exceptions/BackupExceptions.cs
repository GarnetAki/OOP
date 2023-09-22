namespace Backups.Exceptions;

public class BackupExceptions : Exception
{
    private BackupExceptions(string message)
        : base(message)
    {
    }

    public static BackupExceptions RemoveException()
        => new BackupExceptions($"Backup object already not in list.");
}
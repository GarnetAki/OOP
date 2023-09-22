namespace Backups.Exceptions;

public class FileExceptions : Exception
{
    private FileExceptions(string message)
        : base(message)
    {
    }

    public static FileExceptions FileOrFolderAlreadyExists()
        => new FileExceptions($"File or folder already exists.");

    public static FileExceptions FileOrFolderDoesNotExist()
        => new FileExceptions($"File or folder does not exist.");
}
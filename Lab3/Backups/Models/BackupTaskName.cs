namespace Backups.Models;

public class BackupTaskName
{
    public BackupTaskName(string name)
    {
        if (name.Length == 0)
            throw new ArgumentNullException();

        Name = name;
    }

    public string Name { get; }
}
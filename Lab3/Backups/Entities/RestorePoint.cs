using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint : IRestorePoint
{
    private readonly List<BackupObject> _filesList;
    public RestorePoint(IStorage storage, List<BackupObject> copies)
    {
        Storage = storage;
        CreationTime = DateTime.Now;
        _filesList = new List<BackupObject>();
        foreach (BackupObject copy in copies)
        {
            _filesList.Add(copy);
        }
    }

    public DateTime CreationTime { get; }

    public IStorage Storage { get; }

    public IReadOnlyList<BackupObject> BackupObjectsList => _filesList;
}
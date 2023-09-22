using Backups.Entities;

namespace Backups.Interfaces;

public interface IRestorePoint
{
    DateTime CreationTime { get; }

    IStorage Storage { get; }

    IReadOnlyList<BackupObject> BackupObjectsList { get; }
}
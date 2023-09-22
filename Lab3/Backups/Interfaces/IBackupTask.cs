using Backups.Entities;
using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    BackupTaskName Name { get; }

    IBackup Backup { get; }

    void AddObject(BackupObject backupObject);

    IReadOnlyList<BackupObject> BackupObjects();

    void RemoveObject(BackupObject backupObject);

    void StartJob();
}
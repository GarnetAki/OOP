using Backups.Entities;
using Backups.Extra.Logging;
using Backups.Extra.RestoreAlgorithmes;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra;

public class BackupTaskDecorator : IBackupTask, IClean
{
    private BackupTask _backupTask;
    private ICleaner _cleaner;
    private ILogger _logger;

    public BackupTaskDecorator(IClock clock, BackupTask backupTask, ICleaner cleaner, ILogger logger)
    {
        _backupTask = backupTask;
        _cleaner = cleaner;
        _logger = logger;
        clock.AddObserver(this);
    }

    public BackupTaskName Name => _backupTask.Name;

    public IBackup Backup => _backupTask.Backup;

    public void AddObject(BackupObject backupObject)
    {
        _backupTask.AddObject(backupObject);
        _logger.WriteLine($"Backup object {backupObject.GetRepositoryObject().Name} added to list.");
    }

    public IReadOnlyList<BackupObject> BackupObjects()
    {
        return _backupTask.BackupObjects();
    }

    public void RemoveObject(BackupObject backupObject)
    {
        _backupTask.RemoveObject(backupObject);
        _logger.WriteLine($"Backup object {backupObject.GetRepositoryObject().Name} removed from list.");
    }

    public void StartJob()
    {
        _backupTask.StartJob();
        _logger.WriteLine($"Restore point {_backupTask.Backup.RestorePoints[^1].Storage.Path} added to list.");
        _cleaner.Clean(_backupTask, DateTime.MinValue, _logger);
    }

    public void RestoreFiles(string restorePointPath, IRestoreAlgorithm algorithm)
    {
        algorithm.RestoreFiles(_backupTask.Backup.RestorePoints.First(point => point.Storage.Path == restorePointPath));
        _logger.WriteLine($"Restore point {restorePointPath} files restored.");
    }

    public void ClockAlarm(DateTime dateTime)
    {
        _cleaner.Clean(_backupTask, dateTime, _logger);
    }
}
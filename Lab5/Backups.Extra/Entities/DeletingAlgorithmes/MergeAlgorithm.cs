using Backups.Entities;
using Backups.Extra.Logging;
using Backups.Interfaces;

namespace Backups.Extra;

public class MergeAlgorithm : IDeleteAlgorithm
{
    public void Delete(IBackupTask backupTask, List<RestorePoint> restorePoints, RestorePoint newestPoint, ILogger logger)
    {
        var backupObjectsAll = newestPoint.BackupObjectsList.ToList();
        logger.WriteLine($"{backupObjectsAll.Count}");
        foreach (var restorePoint in restorePoints)
        {
            foreach (var backupObject in restorePoint.BackupObjectsList)
            {
                logger.WriteLine($"{backupObject.Path}");
                if (!backupObjectsAll.Contains(backupObject))
                    backupObjectsAll.Add(backupObject);
            }

            backupTask.Backup.RemovePoint(restorePoint);
            restorePoint.Storage.Repository.Delete(restorePoint.Storage.Path);
            logger.WriteLine($"Restore point {restorePoint.Storage.Path} was deleted.");
        }

        logger.WriteLine($"{backupObjectsAll.Count}");

        backupTask.Backup.RemovePoint(newestPoint);
        newestPoint.Storage.Repository.Delete(newestPoint.Storage.Path);
        var backupObjectsWas = new List<BackupObject>();
        while (backupTask.BackupObjects().Count != 0)
        {
            backupObjectsWas.Add(backupTask.BackupObjects()[0]);
            backupTask.RemoveObject(backupTask.BackupObjects()[0]);
        }

        foreach (BackupObject backupObject in backupObjectsAll)
        {
            backupTask.AddObject(backupObject);
        }

        backupTask.StartJob();
        logger.WriteLine($"Deleted restore points merged into {backupTask.Backup.RestorePoints[^1].CreationTime}.");

        while (backupTask.BackupObjects().Count != 0)
        {
            backupTask.RemoveObject(backupTask.BackupObjects()[0]);
        }

        foreach (BackupObject backupObject in backupObjectsWas)
        {
            backupTask.AddObject(backupObject);
        }
    }
}
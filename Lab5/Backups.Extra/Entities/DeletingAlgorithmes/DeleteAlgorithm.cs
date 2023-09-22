using Backups.Entities;
using Backups.Extra.Logging;
using Backups.Interfaces;

namespace Backups.Extra;

public class DeleteAlgorithm : IDeleteAlgorithm
{
    public void Delete(IBackupTask backupTask, List<RestorePoint> restorePoints, RestorePoint newestPoint, ILogger logger)
    {
        foreach (RestorePoint point in restorePoints)
        {
            point.Storage.Repository.Delete(point.Storage.Path);
            backupTask.Backup.RemovePoint(point);
            logger.WriteLine($"Restore point {point.CreationTime} was deleted.");
        }
    }
}
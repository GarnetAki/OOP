using Backups.Entities;
using Backups.Extra.Logging;
using Backups.Interfaces;

namespace Backups.Extra;

public class Cleaner : ICleaner
{
    private IChoosingAlgorithm _choosingAlgorithm;
    private IDeleteAlgorithm _deleteAlgorithm;

    public Cleaner(IChoosingAlgorithm choosingAlgorithm, IDeleteAlgorithm deleteAlgorithm)
    {
        _choosingAlgorithm = choosingAlgorithm;
        _deleteAlgorithm = deleteAlgorithm;
    }

    public void Clean(IBackupTask backupTask, DateTime newTime, ILogger logger)
    {
        RestorePoint newestPoint = backupTask.Backup.RestorePoints[backupTask.Backup.RestorePoints.Count - 1];
        List<RestorePoint> pointsToRemove = _choosingAlgorithm.Choose(backupTask.Backup.RestorePoints, newTime);
        if (pointsToRemove.Contains(newestPoint))
            pointsToRemove.Remove(newestPoint);

        if (pointsToRemove.Count != 0)
            _deleteAlgorithm.Delete(backupTask, pointsToRemove, newestPoint, logger);
    }
}
using Backups.Entities;
using Backups.Extra.Logging;
using Backups.Interfaces;

namespace Backups.Extra;

public interface IDeleteAlgorithm
{
    void Delete(IBackupTask backupTask, List<RestorePoint> restorePoints, RestorePoint newestPoint, ILogger logger);
}
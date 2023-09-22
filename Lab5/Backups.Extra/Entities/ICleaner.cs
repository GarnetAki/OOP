using Backups.Extra.Logging;
using Backups.Interfaces;

namespace Backups.Extra;

public interface ICleaner
{
    void Clean(IBackupTask backupTask, DateTime newTime, ILogger logger);
}
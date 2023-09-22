using Backups.Interfaces;

namespace Backups.Extra;

public interface IClock
{
    List<IClean> ObserverList { get; }

    void AddObserver(IClean backupTask);

    void RemoveObserver(IClean backupTask);

    void NextDay();
}
using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    IReadOnlyList<RestorePoint> RestorePoints { get; }

    void RemovePoint(RestorePoint point);

    void AddPoint(RestorePoint point);
}
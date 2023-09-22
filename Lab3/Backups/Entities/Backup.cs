using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class Backup : IBackup
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

    public void RemovePoint(RestorePoint point)
    {
        if (!_restorePoints.Contains(point))
            throw RestorePointExceptions.RemoveException();

        _restorePoints.Remove(point);
    }

    public void AddPoint(RestorePoint point)
    {
        _restorePoints.Add(point);
    }
}
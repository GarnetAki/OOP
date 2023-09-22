using Backups.Entities;

namespace Backups.Extra.ChoosingAlgorithmes;

public class DateAlgorithm : IChoosingAlgorithm
{
    private TimeSpan _timeSpan;

    public DateAlgorithm(TimeSpan timeSpan)
    {
        _timeSpan = timeSpan;
    }

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints, DateTime time)
    {
        return time.Equals(DateTime.MinValue) ? new List<RestorePoint>() : restorePoints.Where(point => time.Subtract(point.CreationTime).Days > _timeSpan.Days).ToList();
    }
}
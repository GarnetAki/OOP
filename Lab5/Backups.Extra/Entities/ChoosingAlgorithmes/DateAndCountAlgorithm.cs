using Backups.Entities;

namespace Backups.Extra.ChoosingAlgorithmes;

public class DateAndCountAlgorithm : IChoosingAlgorithm
{
    private DateAlgorithm _dateAlgorithm;
    private CountAlgorithm _countAlgorithm;

    public DateAndCountAlgorithm(TimeSpan timeSpan, int count)
    {
        _dateAlgorithm = new DateAlgorithm(timeSpan);
        _countAlgorithm = new CountAlgorithm(count);
    }

    public bool Mode { get; set; } = true;

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints, DateTime time)
    {
        List<RestorePoint> pointsToRemoveByDate = _dateAlgorithm.Choose(restorePoints, time);
        List<RestorePoint> pointsToRemoveByCount = _countAlgorithm.Choose(restorePoints, time);
        var pointsToRemove = new List<RestorePoint>();
        if (Mode)
        {
            pointsToRemove.AddRange(pointsToRemoveByDate.Where(point => pointsToRemoveByCount.Contains(point)));
        }
        else
        {
            pointsToRemove = pointsToRemoveByDate;
            pointsToRemove.AddRange(pointsToRemoveByCount.Where(point => !pointsToRemove.Contains(point)));
        }

        return pointsToRemove;
    }
}
using Backups.Entities;

namespace Backups.Extra.ChoosingAlgorithmes;

public class CountAlgorithm : IChoosingAlgorithm
{
    private int _count;

    public CountAlgorithm(int count)
    {
        _count = count;
    }

    public List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints, DateTime time)
    {
        var pointsToRemove = new List<RestorePoint>();
        for (int i = 0; i < restorePoints.Count - _count; i++)
        {
            pointsToRemove.Add(restorePoints[i]);
        }

        return pointsToRemove;
    }
}
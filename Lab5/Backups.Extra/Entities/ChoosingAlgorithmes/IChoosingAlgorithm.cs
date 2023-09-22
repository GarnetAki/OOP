using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra;

public interface IChoosingAlgorithm
{
    List<RestorePoint> Choose(IReadOnlyList<RestorePoint> restorePoints, DateTime time);
}
using Backups.Entities;

namespace Backups.Extra.RestoreAlgorithmes;

public interface IRestoreAlgorithm
{
    void RestoreFiles(RestorePoint point);
}
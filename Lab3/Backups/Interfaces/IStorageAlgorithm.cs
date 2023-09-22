using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IStorage Backup(string path, IRepository repository, IReadOnlyCollection<IRepositoryObject> objects, IArchiveMaker archiveMaker);
}
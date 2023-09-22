using Backups.Entities.RepositoryAndZipObjects;
using Backups.Interfaces;

namespace Backups.Entities.StorageAlgorithmes;

public class ZipStorage : IStorageAlgorithm
{
    public IStorage Backup(string path, IRepository repository, IReadOnlyCollection<IRepositoryObject> objects, IArchiveMaker archiveMaker)
    {
        return archiveMaker.MakeArchive(path, repository, objects);
    }
}
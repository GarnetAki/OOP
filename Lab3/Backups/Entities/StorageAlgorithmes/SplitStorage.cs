using Backups.Entities.RepositoryAndZipObjects;
using Backups.Interfaces;

namespace Backups.Entities.StorageAlgorithmes;

public class SplitStorage : IStorageAlgorithm
{
    public IStorage Backup(string path, IRepository repository, IReadOnlyCollection<IRepositoryObject> objects, IArchiveMaker archiveMaker)
    {
        var listStorages = new List<IStorage>();
        foreach (IRepositoryObject repositoryObject in objects)
        {
            string newPath = Path.Join(path, repositoryObject.Name + ".zip");
            var repositoryObjects = new List<IRepositoryObject> { repositoryObject };
            listStorages.Add(archiveMaker.MakeArchive(newPath, repository, repositoryObjects));
        }

        return new StorageUnion(listStorages, path, repository);
    }
}
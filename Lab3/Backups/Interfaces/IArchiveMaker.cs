using Backups.Entities.RepositoryAndZipObjects;
using Backups.Interfaces;

namespace Backups.Entities;

public interface IArchiveMaker
{
    IStorage MakeArchive(string path, IRepository repository, IReadOnlyCollection<IRepositoryObject> objects);
}
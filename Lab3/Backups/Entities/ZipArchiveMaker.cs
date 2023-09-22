using System.IO.Compression;
using Backups.Entities.RepositoryAndZipObjects;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipArchiveMaker : IArchiveMaker
{
    public IStorage MakeArchive(string path, IRepository repository, IReadOnlyCollection<IRepositoryObject> objects)
    {
        Stream stream = repository.OpenWrite(path + ".zip");
        var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        IVisitor visitor = new ArchiveVisitor(archive);
        var zipObjects = new List<IZipObject>();
        foreach (IRepositoryObject repositoryObject in objects)
        {
            repositoryObject.Accept(visitor);
        }

        var tmp = new ZipFolder(path + ".zip", zipObjects);
        archive.Dispose();
        return new Storage(path, new List<IZipObject>() { tmp }, repository);
    }
}
using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class Storage : IStorage
{
    private List<IZipObject> _objects;
    public Storage(string path, List<IZipObject> objects, IRepository repository)
    {
        _objects = objects;
        Path = path;
        Repository = repository;
    }

    public List<IZipObject> Objects => _objects;

    public string Path { get; }

    public IRepository Repository { get; }

    public IReadOnlyList<IRepositoryObject> GetObjects()
    {
        var list = new List<IRepositoryObject>();
        Stream archiveStream = Repository.GetFile(Path + ".zip").GetStream();
        var archive = new ZipArchive(archiveStream, ZipArchiveMode.Read);
        foreach (var zipObject in _objects)
        {
            ZipArchiveEntry? objectEntry = archive.GetEntry(zipObject.Name);
            if (objectEntry == null)
                throw FileExceptions.FileOrFolderDoesNotExist();

            list.Add(zipObject.GetRepositoryObject(objectEntry));
        }

        archive.Dispose();
        archiveStream.Dispose();
        return list;
    }
}
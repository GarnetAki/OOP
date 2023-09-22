using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities.RepositoryAndZipObjects;

public class ZipFolder : IZipObject
{
    private IReadOnlyList<IZipObject> _zipObjects;

    public ZipFolder(string name, List<IZipObject> objects)
    {
        _zipObjects = objects;
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        if (!Directory.Exists(zipArchiveEntry.FullName))
            throw FileExceptions.FileOrFolderDoesNotExist();

        Func<IReadOnlyCollection<IRepositoryObject>> Func()
        {
            return () =>
            {
                var zipArchive = new ZipArchive(zipArchiveEntry.Open(), ZipArchiveMode.Read);
                return (IReadOnlyCollection<IRepositoryObject>)zipArchive.Entries.Select(GetRepositoryObject);
            };
        }

        return new Folder(Func(), zipArchiveEntry.Name);
    }
}
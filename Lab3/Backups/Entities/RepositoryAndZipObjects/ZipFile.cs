using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities.RepositoryAndZipObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        if (!System.IO.File.Exists(zipArchiveEntry.FullName))
            throw FileExceptions.FileOrFolderDoesNotExist();

        Func<Stream> Func()
        {
            return zipArchiveEntry.Open;
        }

        return new File(Func(), zipArchiveEntry.Name);
    }
}
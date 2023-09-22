using Backups.Entities.RepositoryAndZipObjects;
using Backups.Exceptions;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class InMemoryRepository : IRepository, IDisposable
{
    private MemoryFileSystem _zioRepository = new MemoryFileSystem();
    public InMemoryRepository()
    {
        _zioRepository.CreateDirectory("/afolder");
        _zioRepository.WriteAllText("/afolder/a.txt", "sad");
        _zioRepository.WriteAllText("/b.txt", "Very sad");
        _zioRepository.WriteAllText("/c.txt", "Very sad");
    }

    public Stream OpenWrite(string path)
    {
        return _zioRepository.OpenFile(new UPath("//" + path), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
    }

    public void CreateDirectory(string path)
    {
        _zioRepository.CreateDirectory(new UPath("//" + path));
    }

    public void Delete(string path)
    {
        if (_zioRepository.FileExists(new UPath("//" + path)))
            _zioRepository.DeleteFile(new UPath("//" + path));

        if (_zioRepository.DirectoryExists(new UPath("//" + path)))
            _zioRepository.DeleteDirectory(new UPath("//" + path), true);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IRepositoryObject GetObject(string path)
    {
        UPath fullPathTmp = new UPath("//" + path).FullName;

        if (_zioRepository.FileExists(new UPath(path)))
        {
            string? name = fullPathTmp.GetName();
            if (name == null)
                throw new ArgumentNullException();

            return new Entities.RepositoryAndZipObjects.File(GetFileFunc(path), name);
        }

        if (_zioRepository.DirectoryExists(new UPath(path)))
        {
            string? name = new DirectoryInfo(path).Name;
            if (name == null)
                throw new ArgumentNullException();

            return new Folder(GetFolderFunc(path), name);
        }

        throw FileExceptions.FileOrFolderDoesNotExist();
    }

    public IFile GetFile(string path)
    {
        UPath fullPathTmp = new UPath("//" + path).FullName;

        if (_zioRepository.FileExists(fullPathTmp))
        {
            string? name = fullPathTmp.GetName();
            if (name == null)
                throw new ArgumentNullException();

            return new Entities.RepositoryAndZipObjects.File(GetFileFunc(path), name);
        }

        throw FileExceptions.FileOrFolderDoesNotExist();
    }

    private Func<Stream> GetFileFunc(string path)
    {
        return () => _zioRepository.OpenFile(new UPath(path), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
    }

    private Func<IReadOnlyCollection<IRepositoryObject>> GetFolderFunc(string path)
    {
        return () =>
        {
            UPath fullPath = new UPath(path).FullName;
            var tmp = _zioRepository.EnumerateFiles(fullPath)
                .Select(file => GetObject(file.FullName)).ToList();

            tmp.AddRange(_zioRepository.EnumerateDirectories(fullPath).Select(directory => GetObject(directory.FullName)));

            return tmp;
        };
    }
}
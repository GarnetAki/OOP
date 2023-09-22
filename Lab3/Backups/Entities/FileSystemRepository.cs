using Backups.Entities.RepositoryAndZipObjects;
using Backups.Exceptions;
using Backups.Interfaces;
using File = Backups.Entities.RepositoryAndZipObjects.File;

namespace Backups.Entities;

public class FileSystemRepository : IRepository
{
    private readonly string _path;

    public FileSystemRepository(string path)
    {
        _path = path;
    }

    public Stream OpenWrite(string path)
    {
        string fullpath = Path.Join(_path, path);

        return new FileStream(fullpath, FileMode.Create);
    }

    public void CreateDirectory(string path)
    {
        string fullpath = Path.Join(_path, path);
        if (!Directory.Exists(fullpath))
            Directory.CreateDirectory(fullpath);
    }

    public void Delete(string path)
    {
        string fullPath = !path.Contains(_path) ? Path.Join(_path, path) : path;
        if (System.IO.File.Exists(fullPath))
            System.IO.File.Delete(fullPath);

        if (System.IO.Directory.Exists(fullPath))
            System.IO.Directory.Delete(fullPath, true);
    }

    public IRepositoryObject GetObject(string path)
    {
        string fullPath = !path.Contains(_path) ? Path.Join(_path, path) : path;

        if (System.IO.File.Exists(fullPath))
        {
            string? name = Path.GetFileName(path);
            if (name == null)
                throw new ArgumentNullException();

            return new File(GetFileFunc(fullPath), name);
        }

        if (Directory.Exists(fullPath))
        {
            string? name = new DirectoryInfo(path).Name;
            if (name == null)
                throw new ArgumentNullException();

            return new Folder(GetFolderFunc(fullPath), name);
        }

        throw FileExceptions.FileOrFolderDoesNotExist();
    }

    public IFile GetFile(string path)
    {
        string fullPath = !path.Contains(_path) ? Path.Join(_path, path) : path;

        if (System.IO.File.Exists(fullPath))
        {
            string? name = Path.GetFileName(path);
            if (name == null)
                throw new ArgumentNullException();

            return new File(GetFileFunc(fullPath), name);
        }

        throw FileExceptions.FileOrFolderDoesNotExist();
    }

    private Func<Stream> GetFileFunc(string path)
    {
        return () => new FileInfo(path).OpenRead();
    }

    private Func<IReadOnlyCollection<IRepositoryObject>> GetFolderFunc(string path)
    {
        return () =>
        {
            var info = new DirectoryInfo(path);
            IEnumerable<FileInfo> files = info.EnumerateFiles();
            IEnumerable<DirectoryInfo> directories = info.EnumerateDirectories();

            var tmp = files.Select(file => GetObject(file.FullName)).ToList();
            tmp.AddRange(directories.Select(directory => GetObject(directory.FullName)));

            return tmp;
        };
    }
}
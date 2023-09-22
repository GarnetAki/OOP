using Backups.Interfaces;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(IRepository repository, string path)
    {
        Repository = repository;
        Path = path;
    }

    public IRepository Repository { get; }

    public string Path { get; }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.GetObject(Path);
    }
}
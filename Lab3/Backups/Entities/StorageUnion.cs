using Backups.Interfaces;

namespace Backups.Entities;

public class StorageUnion : IStorage
{
    private List<IStorage> _storages;

    public StorageUnion(List<IStorage> storages, string path, IRepository repository)
    {
        _storages = storages;
        Path = path;
        Repository = repository;
    }

    public string Path { get; }

    public IRepository Repository { get; }

    public IReadOnlyList<IRepositoryObject> GetObjects()
    {
        return _storages.SelectMany(storage => storage.GetObjects()).ToList();
    }
}
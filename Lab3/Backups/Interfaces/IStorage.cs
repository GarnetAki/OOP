namespace Backups.Interfaces;

public interface IStorage
{
    IRepository Repository { get; }

    string Path { get; }

    IReadOnlyList<IRepositoryObject> GetObjects();
}
namespace Backups.Interfaces;

public interface IRepository
{
    Stream OpenWrite(string path);

    void CreateDirectory(string path);

    void Delete(string path);

    IRepositoryObject GetObject(string path);

    IFile GetFile(string path);
}
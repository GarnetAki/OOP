using Backups.Entities;
using Backups.Extra.RestoreAlgorithmes;
using Backups.Interfaces;

namespace Backups.Extra.Entities.RestoreAlgorithmes;

public class RestoreToDifferent : IRestoreAlgorithm
{
    private IRepository _repository;
    private string _path;

    public RestoreToDifferent(IRepository repository, string path)
    {
        _repository = repository;
        _path = path;
    }

    public void RestoreFiles(RestorePoint point)
    {
        foreach (IRepositoryObject repositoryObject in point.Storage.GetObjects())
        {
            Restore(repositoryObject, _path);
        }
    }

    private void Restore(IRepositoryObject mainRepositoryObject, string path)
    {
        if (mainRepositoryObject is IFile file)
        {
            Stream stream = _repository.OpenWrite(path + "\\" + mainRepositoryObject.Name);
            Stream oldStream = file.GetStream();
            oldStream.CopyTo(stream);
            oldStream.Dispose();
            stream.Dispose();
        }

        if (mainRepositoryObject is IFolder folder)
        {
            _repository.CreateDirectory(path + "\\" + mainRepositoryObject.Name);
            foreach (IRepositoryObject repositoryObject in folder.GetContent())
            {
                Restore(repositoryObject, path + "\\" + mainRepositoryObject.Name);
            }
        }
    }
}
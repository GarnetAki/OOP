using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra.RestoreAlgorithmes;

public class RestoreToOriginal : IRestoreAlgorithm
{
    public void RestoreFiles(RestorePoint point)
    {
        foreach (IRepositoryObject repositoryObject in point.Storage.GetObjects())
        {
            var backupObject = point.BackupObjectsList.First(o => o.GetRepositoryObject() == repositoryObject);
            Restore(repositoryObject, backupObject.Path, backupObject.Repository);
        }
    }

    private void Restore(IRepositoryObject mainRepositoryObject, string path, IRepository repository)
    {
        if (mainRepositoryObject is IFile file)
        {
            Stream stream = repository.OpenWrite(path + "\\" + mainRepositoryObject.Name);
            Stream oldStream = file.GetStream();
            oldStream.CopyTo(stream);
            oldStream.Dispose();
            stream.Dispose();
        }

        if (mainRepositoryObject is IFolder folder)
        {
            repository.CreateDirectory(path + "\\" + mainRepositoryObject.Name);
            foreach (IRepositoryObject repositoryObject in folder.GetContent())
            {
                Restore(repositoryObject, path + "\\" + mainRepositoryObject.Name, repository);
            }
        }
    }
}
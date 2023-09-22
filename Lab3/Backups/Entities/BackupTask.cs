using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private IStorageAlgorithm _storageAlgorithm;
    private IRepository _repository;
    private IArchiveMaker _archiveMaker;
    private List<BackupObject> _filesToBackup;
    private int _numberStart;

    public BackupTask(BackupTaskName name, IRepository repository, IStorageAlgorithm storageAlgorithm, List<BackupObject> files, IArchiveMaker archiveMaker, int numberStart)
    {
        Name = name;
        _numberStart = numberStart;
        _storageAlgorithm = storageAlgorithm;
        _filesToBackup = files;
        _archiveMaker = archiveMaker;
        _repository = repository;
    }

    public IBackup Backup { get; } = new Backup();

    public BackupTaskName Name { get; }

    public void AddObject(BackupObject backupObject)
    {
        _filesToBackup.Add(backupObject);
    }

    public IReadOnlyList<BackupObject> BackupObjects()
    {
        return _filesToBackup;
    }

    public void RemoveObject(BackupObject backupObject)
    {
        if (!_filesToBackup.Contains(backupObject))
            throw BackupExceptions.RemoveException();

        _filesToBackup.Remove(backupObject);
    }

    public void StartJob()
    {
        _repository.CreateDirectory(Name.Name + _numberStart);
        var objects = _filesToBackup.Select(file => file.GetRepositoryObject()).ToList();
        Backup.AddPoint(new RestorePoint(_storageAlgorithm.Backup(Name.Name + _numberStart, _repository, objects, _archiveMaker), _filesToBackup));
        _numberStart++;
    }
}
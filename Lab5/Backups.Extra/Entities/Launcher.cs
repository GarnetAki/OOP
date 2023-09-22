using Backups.Entities;
using Backups.Entities.StorageAlgorithmes;
using Backups.Extra.ChoosingAlgorithmes;
using Backups.Extra.Entities.RestoreAlgorithmes;
using Backups.Extra.Logging;
using Backups.Extra.RestoreAlgorithmes;
using Backups.Models;
using Backups.Test;
using Xunit;

namespace Backups.Extra;

public class Launcher
{
    public static void Main(string[] args)
    {
        var inMemoryRepository = new FileSystemRepository("C:\\Repo");
        var aMaker = new ZipArchiveMaker();
        var algo = new SplitStorage();
        var backupTask = new BackupTask(new BackupTaskName("abob"), inMemoryRepository, algo, new List<BackupObject>(), aMaker, 1);
        var clock = new VirtualClock();
        var cleaner = new Cleaner(new CountAlgorithm(2), new MergeAlgorithm());
        var logger = new FileLogger(inMemoryRepository, "\\log.txt");
        var decorator = new BackupTaskDecorator(clock, backupTask, cleaner, logger);

        var aFolder = new BackupObject(inMemoryRepository, "\\afolder");
        var b = new BackupObject(inMemoryRepository, "\\b.txt");
        var c = new BackupObject(inMemoryRepository, "\\c.txt");

        decorator.AddObject(aFolder);
        decorator.AddObject(b);
        decorator.StartJob();
        decorator.RemoveObject(b);
        decorator.StartJob();
        decorator.AddObject(c);
        decorator.StartJob();
    }
}
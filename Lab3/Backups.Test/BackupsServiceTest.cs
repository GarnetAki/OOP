using Backups.Entities;
using Backups.Entities.StorageAlgorithmes;
using Backups.Models;
using Xunit;
using Xunit.Abstractions;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class BackupsServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BackupsServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void InMemoryRepositoryAndSplitAlgorythm()
    {
        var inMemoryRepository = new InMemoryRepository();
        var aMaker = new ZipArchiveMaker();
        var algo = new SplitStorage();
        var backupTask = new BackupTask(new BackupTaskName("aboba"), inMemoryRepository, algo, new List<BackupObject>(), aMaker, 1);
        var aFolder = new BackupObject(inMemoryRepository, "//afolder");
        var b = new BackupObject(inMemoryRepository, "//b.txt");

        backupTask.AddObject(aFolder);
        backupTask.AddObject(b);
        backupTask.StartJob();
        backupTask.RemoveObject(aFolder);
        backupTask.StartJob();
        Assert.True(backupTask.Backup.RestorePoints.Count == 2);
    }
}
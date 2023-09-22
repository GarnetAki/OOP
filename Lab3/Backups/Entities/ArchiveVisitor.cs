using System.IO.Compression;
using Backups.Entities.RepositoryAndZipObjects;
using Backups.Interfaces;
using Microsoft.VisualBasic;
using ZipFile = Backups.Entities.RepositoryAndZipObjects.ZipFile;

namespace Backups.Entities;

public class ArchiveVisitor : IVisitor
{
    private Stack<ZipArchive> _stack = new Stack<ZipArchive>();

    public ArchiveVisitor(ZipArchive archive)
    {
        _stack.Push(archive);
    }

    public void VisitFolder(IFolder repositoryObject)
    {
        ZipArchive arch = _stack.Peek();
        ZipArchiveEntry entry = arch.CreateEntry(repositoryObject.Name + ".zip");
        Stream entryStream = entry.Open();
        var newArch = new ZipArchive(entryStream, ZipArchiveMode.Create);
        _stack.Push(newArch);
        foreach (var item in repositoryObject.GetContent())
        {
            item.Accept(this);
        }

        newArch.Dispose();
        _stack.Pop();
    }

    public void VisitFile(IFile repositoryObject)
    {
        ZipArchive archive = _stack.Peek();
        ZipArchiveEntry entry = archive.CreateEntry(repositoryObject.Name);
        using (Stream stream = entry.Open())
        {
            using (Stream repositoryStream = repositoryObject.GetStream())
            {
                repositoryStream.CopyTo(stream);
            }
        }

        var zipFile = new ZipFile(repositoryObject.Name);
    }
}
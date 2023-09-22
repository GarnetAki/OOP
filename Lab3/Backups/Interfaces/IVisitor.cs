namespace Backups.Interfaces;

public interface IVisitor
{
    void VisitFolder(IFolder repositoryObject);

    void VisitFile(IFile repositoryObject);
}
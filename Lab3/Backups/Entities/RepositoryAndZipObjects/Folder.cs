using Backups.Interfaces;

namespace Backups.Entities.RepositoryAndZipObjects;

public class Folder : IFolder
{
    private readonly Func<IReadOnlyCollection<IRepositoryObject>> _func;
    private readonly string _name;

    public Folder(Func<IReadOnlyCollection<IRepositoryObject>> func, string name)
    {
        if (name.Length == 0)
            throw new ArgumentNullException();

        _name = name;
        _func = func;
    }

    public string Name => _name;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitFolder(this);
    }

    public IReadOnlyCollection<IRepositoryObject> GetContent()
    {
        return _func.Invoke();
    }
}
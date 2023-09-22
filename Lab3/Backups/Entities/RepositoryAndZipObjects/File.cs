using Backups.Interfaces;

namespace Backups.Entities.RepositoryAndZipObjects;

public class File : IFile
{
    private readonly Func<Stream> _func;
    private readonly string _name;

    public File(Func<Stream> func, string name)
    {
        _name = name;
        _func = func;
    }

    public string Name => _name;

    public void Accept(IVisitor visitor)
    {
        visitor.VisitFile(this);
    }

    public Stream GetStream()
    {
        return _func.Invoke();
    }
}
using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public class DataHolder
{
    private List<StudentWrapper> _studentWrappers;

    public DataHolder()
    {
        _studentWrappers = new List<StudentWrapper>();
    }

    public IReadOnlyList<StudentWrapper> StudentWrappers => _studentWrappers;

    public void AddStudent(StudentWrapper studentWrapper)
    {
        if (studentWrapper == null)
            throw new ArgumentNullException();

        _studentWrappers.Add(studentWrapper);
    }
}
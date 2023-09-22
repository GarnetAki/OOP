using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private GroupName _groupName;
    private List<Student> _students;

    public Group(GroupName groupName)
    {
        var students = new List<Student>();
        _groupName = groupName;
        _students = students;
        GroupName = groupName;
    }

    public GroupName GroupName { get; }
    public IReadOnlyList<Student> Students => _students;

    public int MaxStudentsCnt { get; } = 30;

    public void RemoveStudent(Student student)
    {
        if (!_students.Any(students => students == student))
            StudentNotFoundException.Throw(student.Id.Id, GroupName);
        _students.Remove(student);
    }

    public void AddNewStudent(Student student)
    {
        if (MaxStudentsCnt < _students.Count + 1)
            GroupIsFullException.Throw(GroupName);
        if (Students.Contains(student))
            StudentAlreadyInGroupException.Throw(student.Id.Id);
        _students.Add(student);
    }

    public bool Equals(Group? other)
    {
        return GroupName == other?.GroupName;
    }
}
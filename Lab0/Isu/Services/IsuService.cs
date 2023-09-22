using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService
{
    private List<Group> _allGroups;
    private List<Student> _allStudents;
    private IdGenerator _studentIdGenerator;

    public IsuService()
    {
        _allGroups = new List<Group>();
        _allStudents = new List<Student>();
        _studentIdGenerator = new IdGenerator();
    }

    public Group AddGroup(GroupName name)
    {
        if (_allGroups.Any(group => group.GroupName == name))
            GroupAlreadyExistException.Throw(name);
        var newGroup = new Group(name);
        _allGroups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        StudentId newStudentId = _studentIdGenerator.NextId();
        var formatName = new StudentName(name);
        var newStudent = new Student(newStudentId, group, formatName);

        _allStudents.Add(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        return _allStudents.Single(student => student.Id.Id == id);
    }

    public Student? FindStudent(int id)
    {
        return _allStudents.SingleOrDefault(student => student.Id == new StudentId(id));
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        IReadOnlyList<Student>? list = _allGroups.SingleOrDefault(group => group.GroupName == groupName)?.Students;
        return list ?? new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _allStudents.Where(student => student.IsuGroup.GroupName.CourseNumber == courseNumber).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _allGroups.SingleOrDefault(group => group.GroupName == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return (List<Group>)_allGroups.Where(group => group.GroupName.CourseNumber == courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (newGroup.Equals(student.IsuGroup))
            TheSameGroupException.Throw(newGroup.GroupName);
        student.ChangeGroup(newGroup);
    }
}
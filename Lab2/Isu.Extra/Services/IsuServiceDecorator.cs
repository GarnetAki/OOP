using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceDecorator : IIsuService
{
    private IsuService _decoratee;
    private List<IsuGroupProector> _isuGroupProectors;
    private DataHolder _dataHolder;

    public IsuServiceDecorator(IsuService decoratee, DataHolder dataHolder)
    {
        _decoratee = decoratee;
        _dataHolder = dataHolder;
        _isuGroupProectors = new List<IsuGroupProector>();
    }

    public Group AddGroup(GroupName name)
    {
        Group group = _decoratee.AddGroup(name);
        _isuGroupProectors.Add(new IsuGroupProector(name, new Schedule()));
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        Student student = _decoratee.AddStudent(group, name);
        IsuGroupProector isuGroupProector = _isuGroupProectors.Single(gr => gr.GroupName == group.GroupName);
        _dataHolder.AddStudent(new StudentWrapper(student, isuGroupProector));
        return student;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        IsuGroupProector newGroupProector = _isuGroupProectors.Single(group => group.GroupName == newGroup.GroupName);
        StudentWrapper studentWrapper = _dataHolder.StudentWrappers.Single(stud => stud.Student == student);
        if (!studentWrapper.CheckPossibilityChangeIsuGroup(newGroupProector))
            throw ServiceException.CreateNewGroupScheduleConflictsWithThisStudentSchedule(student);

        _decoratee.ChangeStudentGroup(student, newGroup);
        studentWrapper.IsuGroupProector = newGroupProector;
    }

    public Student? FindStudent(int id)
        => _decoratee.FindStudent(id);

    public List<Student> FindStudents(GroupName groupName)
        => (List<Student>)_decoratee.FindStudents(groupName);

    public List<Student> FindStudents(CourseNumber courseNumber)
        => _decoratee.FindStudents(courseNumber);

    public Group? FindGroup(GroupName groupName)
        => _decoratee.FindGroup(groupName);

    public List<Group> FindGroups(CourseNumber courseNumber)
        => _decoratee.FindGroups(courseNumber);

    public Student GetStudent(int id)
        => _decoratee.GetStudent(id);
}
using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Student
{
    public Student(StudentId id, Group group, StudentName fullname)
    {
        if (group.Students.Any(student => student.Id == id))
            StudentAlreadyInGroupException.Throw(id.Id);
        group.AddNewStudent(this);
        Id = id;
        Name = fullname;
        IsuGroup = group;
    }

    public StudentName Name { get; }

    public StudentId Id { get; }

    public Group IsuGroup { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        Group oldGroup = this.IsuGroup;
        newGroup.AddNewStudent(this);
        oldGroup.RemoveStudent(this);
        IsuGroup = newGroup;
    }
}
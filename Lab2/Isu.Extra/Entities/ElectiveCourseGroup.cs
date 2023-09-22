using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;

namespace Isu.Extra.Entities;

public class ElectiveCourseGroup
{
    private List<Student> _students;

    public ElectiveCourseGroup(ElectiveCourseGroupName electiveCourseGroupName, Schedule schedule, Course course, StudyStream stream)
    {
        if (schedule == null)
            throw new ArgumentNullException();

        ValidateCourse(course, stream, electiveCourseGroupName);
        stream.AddGroup(this);
        ElectiveCourseGroupName = electiveCourseGroupName;
        Schedule = schedule;
        Course = course;
        _students = new List<Student>();
    }

    public ElectiveCourseGroupName ElectiveCourseGroupName { get; }

    public Course Course { get; }

    public Schedule Schedule { get; set; }

    public IReadOnlyList<Student> Students => _students;

    public int MaxStudentsCount { get; private set; } = 30;

    public void ChangeMaxStudentCount(int newMaxStudentCount)
    {
        if (_students.Count > newMaxStudentCount)
            throw GroupException.CreateMaxStudentCountCanNotBeNegativeNumber();

        if (newMaxStudentCount < 1)
            throw GroupException.CreateMaxStudentCountCanNotBeNegativeNumber();

        MaxStudentsCount = newMaxStudentCount;
    }

    public void RemoveStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException();

        if (_students.All(students => students != student))
            throw GroupException.CreateStudentAlreadyNotInGroup();

        _students.Remove(student);
    }

    public void AddNewStudent(StudentWrapper student)
    {
        if (student == null)
            throw new ArgumentNullException();

        if (MaxStudentsCount < _students.Count + 1)
            throw GroupException.CreateGroupStudentsReceiveLimit();

        if (Students.Contains(student.Student))
            throw GroupException.CreateStudentAlreadyInGroup();

        _students.Add(student.Student);
    }

    private void ValidateCourse(Course course, StudyStream stream, ElectiveCourseGroupName name)
    {
        if (course == null)
            throw new ArgumentNullException();

        if (stream == null)
            throw new ArgumentNullException();

        if (name == null)
            throw new ArgumentNullException();

        if (!course.StudyStreams.Contains(stream))
            throw GroupException.CreateCourseDoesNotContainsStream();

        if (name.Stream != stream.StreamNumber)
            throw GroupException.CreateStreamNameAndStreamNumberNotEquals();
    }
}
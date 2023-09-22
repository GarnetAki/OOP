using System.Diagnostics;
using Isu.Services;

namespace Isu.Models;

public class GroupName
{
    public GroupName(string name)
    {
        if (name.Length != 6)
            GroupNameFormatException.ThrowFormat(name);

        var faculty = new GroupFaculty(name[0]);
        var number = new GroupNumber(name);
        var courseNumber = new CourseNumber(name);
        CourseNumber = courseNumber;
        Faculty = faculty;
        Number = number.Number;
    }

    public GroupFaculty Faculty { get; }

    public string Number { get; }

    public CourseNumber CourseNumber { get; }

    public override string ToString()
    {
        return Faculty.Faculty + Number + CourseNumber.Course.ToString();
    }
}
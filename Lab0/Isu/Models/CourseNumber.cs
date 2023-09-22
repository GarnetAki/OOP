using Isu.Services;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(string name)
    {
        int courseNumber = int.Parse(name.Substring(5, 1));
        ValidateCourseNumber(courseNumber);
        Course = courseNumber;
    }

    public int Course { get; }

    private static void ValidateCourseNumber(int courseNumber)
    {
        if (courseNumber is < 1 or > 7)
            GroupNameFormatException.ThrowCourse(courseNumber);
    }
}
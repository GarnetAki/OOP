namespace Isu.Extra.Exceptions;

public class CourseFacultyException : CourseException
{
    private CourseFacultyException(string message)
        : base(message)
    {
    }

    public static CourseFacultyException CreateFacultyDoesNotExist(string str)
        => new CourseFacultyException($"Faculty {str} does not exist.");
}
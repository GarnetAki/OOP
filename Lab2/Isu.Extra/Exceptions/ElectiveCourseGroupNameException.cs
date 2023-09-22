namespace Isu.Extra.Exceptions;

public class ElectiveCourseGroupNameException : GroupException
{
    private ElectiveCourseGroupNameException(string message)
        : base(message)
    {
    }

    public static ElectiveCourseGroupNameException CreateNameIsIncorrect()
        => new ElectiveCourseGroupNameException($"Group name is incorrect.");
}
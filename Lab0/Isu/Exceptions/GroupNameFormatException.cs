namespace Isu.Services;

public class GroupNameFormatException : GroupExceptions
{
    public GroupNameFormatException(string message)
        : base(message)
    {
    }

    public static void ThrowFaculty(char faculty)
    {
        throw new GroupNameFormatException($"Group faculty \"{faculty}\" is not correct.");
    }

    public static void ThrowFormat(string name)
    {
        throw new GroupNameFormatException($"Group format is not correct. (\"{name}\")");
    }

    public static void ThrowCourse(int course)
    {
        throw new GroupNameFormatException($"Group course number \"{course}\" is not correct.");
    }
}
namespace Isu.Extra.Exceptions;

public class CourseException : IsuServiceException
{
    protected CourseException(string message)
        : base(message)
    {
    }

    public static CourseException CreateStudyStreamsDoesNotContainsStream()
        => new CourseException($"Study stream does not contains stream.");

    public static CourseException CreateStudyStreamGroupsNumbersRepeat()
        => new CourseException($"Study stream groups numbers repeat.");
}
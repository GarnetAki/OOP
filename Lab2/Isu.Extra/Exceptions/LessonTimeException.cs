namespace Isu.Extra.Exceptions;

public class LessonTimeException : ServiceException
{
    private LessonTimeException(string message)
        : base(message)
    {
    }

    public static LessonTimeException CreateLessonTimeDayOrIndexInvalid()
        => new LessonTimeException($"Lesson time day or index is invalid.");
}
namespace Isu.Extra.Exceptions;

public class ScheduleException : ServiceException
{
    private ScheduleException(string message)
        : base(message)
    {
    }

    public static ScheduleException CreateLessonOnThisTimeAlreadyExist()
        => new ScheduleException($"Lesson on this time already exist.");
}
namespace Isu.Extra.Exceptions;

public class GroupException : IsuServiceException
{
    protected GroupException(string message)
        : base(message)
    {
    }

    public static GroupException CreateCourseAlreadyExists()
        => new GroupException($"This group already contains course.");

    public static GroupException CreateGroupIsNotTheSame()
        => new GroupException($"Group and group proector are different.");

    public static GroupException CreateNewMaxStudentCountLowerThanCurrent()
        => new GroupException($"Current students count more than new maximum.");

    public static GroupException CreateMaxStudentCountCanNotBeNegativeNumber()
        => new GroupException($"Number can not be negative.");

    public static GroupException CreateStudentAlreadyNotInGroup()
        => new GroupException($"Student already do not study in this group.");

    public static GroupException CreateCourseDoesNotContainsStream()
        => new GroupException($"Course does not contains stream.");

    public static GroupException CreateStreamNameAndStreamNumberNotEquals()
        => new GroupException($"Stream name and stream number does not equal.");

    public static GroupException CreateStudentAlreadyInGroup()
        => new GroupException($"Student already study in this group.");

    public static GroupException CreateGroupStudentsReceiveLimit()
        => new GroupException($"Group receive students limit.");

    public static GroupException CreateNewScheduleIsNotСompatibilityWithStudentsSchedule()
        => new GroupException($"New schedule is not compatibility with students schedules.");
}
using Isu.Entities;

namespace Isu.Extra.Exceptions;

public class ServiceException : IsuServiceException
{
    protected ServiceException(string message)
        : base(message)
    {
    }

    public static ServiceException CreateCourseAlreadyExists()
        => new ServiceException($"Course with this name already exists.");

    public static ServiceException CreateStudentAlreadyInvolveInTwoCourses(Student student)
        => new ServiceException($"Student {student.Name.NameToString()} already involve in two courses.");

    public static ServiceException CreateCourseInSameFaculty(Student student)
        => new ServiceException($"Student {student.Name.NameToString()} learning in the same faculty as the course.");

    public static ServiceException CreateStudentAlreadyInvolveInThisCourse(Student student)
        => new ServiceException($"Student {student.Name.NameToString()} already learn in this course.");

    public static ServiceException CreateDoesNotExistGroupWithСompatibilitySchedule()
        => new ServiceException($"The group with compatibility schedule does not exist.");

    public static ServiceException CreateStudentAlreadyDoesNotInvolvedInThisCourse(Student student)
        => new ServiceException($"Student {student.Name.NameToString()} already does not learns in this course.");

    public static ServiceException CreateNewGroupScheduleConflictsWithThisStudentSchedule(Student student)
        => new ServiceException($"Student {student.Name.NameToString()} have conflict with new group schedule.");
}
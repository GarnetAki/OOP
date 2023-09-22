using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class StudentWrapper
{
    public const int MaxElectiveCourseGroupCount = 2;
    public StudentWrapper(Student student, IsuGroupProector isuGroup)
    {
        Student = student ?? throw new ArgumentNullException();
        ElectiveCourseGroups = new List<ElectiveCourseGroup>();
        IsuGroupProector = isuGroup ?? throw new ArgumentNullException();
    }

    public Student Student { get; }

    public List<ElectiveCourseGroup> ElectiveCourseGroups { get; }

    public IsuGroupProector IsuGroupProector { get; internal set; }

    public bool CheckPossibilityAddGroup(ElectiveCourseGroup group)
    {
        if (group == null)
            throw new ArgumentNullException();

        return group.Schedule.CheckСompatibility(IsuGroupProector.Schedule)
               && ElectiveCourseGroups.All(electiveGroup => electiveGroup.Schedule.CheckСompatibility(group.Schedule));
    }

    public bool CheckPossibilityChangeIsuGroup(IsuGroupProector group)
    {
        if (group == null)
            throw new ArgumentNullException();

        return ElectiveCourseGroups.All(electiveGroup => electiveGroup.Schedule.CheckСompatibility(group.Schedule));
    }

    public bool CheckPossibilityChangeElectiveGroupSchedule(ElectiveCourseGroup group, Schedule schedule)
    {
        if (group == null)
            throw new ArgumentNullException();

        if (schedule == null)
            throw new ArgumentNullException();

        if (ElectiveCourseGroups.Count == MaxElectiveCourseGroupCount)
            return schedule.CheckСompatibility(IsuGroupProector.Schedule) && schedule.CheckСompatibility(ElectiveCourseGroups.Single(gr => gr != group).Schedule);
        else
            return schedule.CheckСompatibility(IsuGroupProector.Schedule);
    }

    public void AddElectiveCourseGroup(ElectiveCourseGroup group, string faculty)
    {
        if (group == null)
            throw new ArgumentNullException();

        if (ElectiveCourseGroups.Count == MaxElectiveCourseGroupCount)
            throw ServiceException.CreateStudentAlreadyInvolveInTwoCourses(Student);

        if (faculty == group.Course.Faculty.Faculty)
            throw ServiceException.CreateCourseInSameFaculty(Student);

        if (ElectiveCourseGroups.Any(electiveGroup => group.Course == electiveGroup.Course))
            throw ServiceException.CreateStudentAlreadyInvolveInThisCourse(Student);

        group.AddNewStudent(this);
        ElectiveCourseGroups.Add(group);
    }

    public void RemoveFromGroup(ElectiveCourseGroup group)
    {
        if (group == null)
            throw new ArgumentNullException();

        ElectiveCourseGroups.Remove(group);
    }
}
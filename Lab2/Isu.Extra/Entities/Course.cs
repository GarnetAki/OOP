using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Course
{
    private List<StudyStream> _studyStreams;

    public Course(string courseName, CourseFaculty faculty)
    {
        CourseName = courseName;
        Faculty = faculty ?? throw new ArgumentNullException();
        _studyStreams = new List<StudyStream>();
    }

    public IReadOnlyList<StudyStream> StudyStreams => _studyStreams;

    public string CourseName { get; }

    public CourseFaculty Faculty { get; }

    public void AddStudyStream(StudyStream stream)
    {
        ValidateCourse(stream);
        _studyStreams.Add(stream);
    }

    public ElectiveCourseGroup FindGroup(StudentWrapper studentWrapper)
    {
        if (studentWrapper == null)
            throw new ArgumentNullException();

        ElectiveCourseGroup? newGroup = StudyStreams.FirstOrDefault(str => str.ElectiveCourseGroups
                .Any(studentWrapper.CheckPossibilityAddGroup))
            ?.ElectiveCourseGroups.FirstOrDefault(studentWrapper.CheckPossibilityAddGroup);

        if (newGroup == null)
            throw ServiceException.CreateDoesNotExistGroupWithСompatibilitySchedule();

        return newGroup;
    }

    public ElectiveCourseGroup FindGroupWithStudent(StudentWrapper studentWrapper)
    {
        if (studentWrapper == null)
            throw new ArgumentNullException();

        ElectiveCourseGroup? newGroup = StudyStreams.SingleOrDefault(str => str.ElectiveCourseGroups
                .Any(gr => gr.Students.Contains(studentWrapper.Student)))?.ElectiveCourseGroups
                    .SingleOrDefault(gr => gr.Students.Contains(studentWrapper.Student));

        if (newGroup == null)
            throw ServiceException.CreateStudentAlreadyDoesNotInvolvedInThisCourse(studentWrapper.Student);
        else
            return newGroup;
    }

    private void ValidateCourse(StudyStream stream)
    {
        if (stream == null)
            throw new ArgumentNullException();

        if (StudyStreams.Where(str => !str.Equals(stream)).Any(str => str.StreamNumber == stream.StreamNumber))
            throw CourseException.CreateStudyStreamGroupsNumbersRepeat();
    }
}
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private Dictionary<char, string> _facultyDescription;
    private ProfessorIdGenerator _professorIdGenerator;
    private List<Course> _courses;
    private DataHolder _dataHolder;

    public IsuExtraService(ProfessorIdGenerator professorIdGenerator, DataHolder dataHolder)
    {
        _professorIdGenerator = professorIdGenerator;
        _dataHolder = dataHolder;
        _facultyDescription = new Dictionary<char, string>()
        {
            { 'M', "TInT" },
            { 'A', "CTM" },
            { 'P', "PT" },
            { 'B', "BTLS" },
            { 'I', "ICT and IIDP" },
            { 'T', "TMI" },
            { 'K', "CTM" },
            { 'N', "TInT" },
        };
        _courses = new List<Course>();
    }

    public Course CreateCourse(Course course)
    {
        if (_courses.Any(existCourse => existCourse.CourseName == course.CourseName))
            throw ServiceException.CreateCourseAlreadyExists();
        _courses.Add(course);
        return course;
    }

    public void AddStudentInCourse(Student student, Course course)
    {
        StudentWrapper studentWrapper = _dataHolder.StudentWrappers.Single(stud => stud.Student == student);
        ElectiveCourseGroup newGroup = course.FindGroup(studentWrapper);
        string faculty = _facultyDescription[studentWrapper.IsuGroupProector.GroupName.Faculty.Faculty];
        studentWrapper.AddElectiveCourseGroup(newGroup, faculty);
    }

    public void LeaveStudentFromCourse(Student student, Course course)
    {
        StudentWrapper studentWrapper = _dataHolder.StudentWrappers.Single(stud => stud.Student == student);
        ElectiveCourseGroup group = course.FindGroupWithStudent(studentWrapper);
        group.RemoveStudent(student);
        studentWrapper.RemoveFromGroup(group);
    }

    public IReadOnlyList<StudyStream> StreamsOfCourse(Course course)
    {
        return course.StudyStreams;
    }

    public List<Student> StudentsFromCourse(Course course)
    {
        return (from stream in course.StudyStreams
            from electiveCourseGroup in stream.ElectiveCourseGroups
            from student in electiveCourseGroup.Students
            select student).ToList();
    }

    public List<Student> StudentsWithoutTwoCourses(Group group)
    {
        return group.Students.Where(student => _dataHolder.StudentWrappers.Single(wrap => wrap.Student == student).ElectiveCourseGroups.Count != 2).ToList();
    }

    public void UpdateSchedule(Schedule.ScheduleBuilder builder, ElectiveCourseGroup group)
    {
        if (builder == null)
            throw new ArgumentNullException();
        if (group == null)
            throw new ArgumentNullException();

        Schedule schedule = builder.Build();
        if (_dataHolder.StudentWrappers.Where(studentWrapper => group.Students.Contains(studentWrapper.Student)).Any(studentWrapper => studentWrapper.CheckPossibilityChangeElectiveGroupSchedule(group, schedule)))
            throw GroupException.CreateNewScheduleIsNotСompatibilityWithStudentsSchedule();

        group.Schedule = schedule;
    }

    public void UpdateScheduleIsu(Schedule.ScheduleBuilder builder, IsuGroupProector isuGroup, Group group)
    {
        if (builder == null)
            throw new ArgumentNullException();

        if (group == null)
            throw new ArgumentNullException();

        if (isuGroup.GroupName != group.GroupName)
            throw GroupException.CreateGroupIsNotTheSame();

        Schedule schedule = builder.Build();
        if (_dataHolder.StudentWrappers.Where(studentWrapper => group.Students.Contains(studentWrapper.Student)).Any(studentWrapper => studentWrapper.CheckPossibilityChangeIsuGroup(isuGroup)))
            throw GroupException.CreateNewScheduleIsNotСompatibilityWithStudentsSchedule();

        isuGroup.Schedule = schedule;
    }
}
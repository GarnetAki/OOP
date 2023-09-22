using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private IsuExtraService _isuExtraService;
    private IsuServiceDecorator _isuServiceDecorator;
    private DataHolder _dataHolder;
    public IsuExtraServiceTest()
    {
        _dataHolder = new DataHolder();
        _isuExtraService = new IsuExtraService(new ProfessorIdGenerator(), _dataHolder);
        _isuServiceDecorator = new IsuServiceDecorator(new IsuService(), _dataHolder);
    }

    [Fact]
    public void CreateNewCourseAndCourseExist()
    {
        var lesson = new Lesson(new Audience(new Address("Lomo 5"), 222), new ProfessorId(121212), new LessonTime(1, 1));
        var listOfLessons = new List<Lesson>();
        listOfLessons.Add(lesson);
        Schedule schedule = new Schedule.ScheduleBuilder(listOfLessons).Build();

        var course = new Course("КИБ", new CourseFaculty("TInT"));
        _isuExtraService.CreateCourse(course);
        var stream = new StudyStream(1, course);
        Assert.Contains(course.StudyStreams, studyStream => studyStream == stream);

        var group = new ElectiveCourseGroup(new ElectiveCourseGroupName("КИБ", 1, 1), schedule, course, stream);
        Assert.Contains(stream.ElectiveCourseGroups, eGroup => eGroup == group);
    }

    [Fact]
    public void StudentTakeCourseAndRemoveCourseAndCheckStudentsOnCourse()
    {
        var lesson = new Lesson(new Audience(new Address("Lomo 5"), 222), new ProfessorId(121212), new LessonTime(1, 1));
        var listOfLessons = new List<Lesson>();
        listOfLessons.Add(lesson);
        Schedule schedule = new Schedule.ScheduleBuilder(listOfLessons).Build();
        var groupName = new GroupName("B32001");
        Group isuGroup = _isuServiceDecorator.AddGroup(groupName);
        Student student = _isuServiceDecorator.AddStudent(isuGroup, "Vasya Vas Vus");
        var course = new Course("КИБ", new CourseFaculty("TInT"));
        _isuExtraService.CreateCourse(course);
        var stream = new StudyStream(1, course);
        var group = new ElectiveCourseGroup(new ElectiveCourseGroupName("КИБ", 1, 1), schedule, course, stream);

        Assert.Contains(course.StudyStreams, studyStream => studyStream == stream);
        _isuExtraService.AddStudentInCourse(student, course);
        Assert.Contains(_isuExtraService.StudentsFromCourse(course), st => st.Name == student.Name);
        _isuExtraService.LeaveStudentFromCourse(student, course);
        Assert.DoesNotContain(_isuExtraService.StudentsFromCourse(course), st => st.Name == student.Name);
    }

    [Fact]
    public void StudentListWithoutTwoCourses()
    {
        var lesson = new Lesson(new Audience(new Address("Lomo 5"), 222), new ProfessorId(121212), new LessonTime(1, 1));
        var listOfLessons = new List<Lesson>();
        listOfLessons.Add(lesson);
        Schedule schedule = new Schedule.ScheduleBuilder(listOfLessons).Build();
        var groupName = new GroupName("B32001");
        Group isuGroup = _isuServiceDecorator.AddGroup(groupName);
        Student student = _isuServiceDecorator.AddStudent(isuGroup, "Petya Pas Pus");
        var course = new Course("КИБ", new CourseFaculty("TInT"));
        _isuExtraService.CreateCourse(course);
        var stream = new StudyStream(1, course);
        var group = new ElectiveCourseGroup(new ElectiveCourseGroupName("КИБ", 1, 1), schedule, course, stream);

        var lesson1 = new Lesson(new Audience(new Address("Lomo 5"), 222), new ProfessorId(121212), new LessonTime(3, 2));
        var listOfLessons1 = new List<Lesson>();
        listOfLessons1.Add(lesson1);
        Schedule schedule1 = new Schedule.ScheduleBuilder(listOfLessons1).Build();
        Student student1 = _isuServiceDecorator.AddStudent(isuGroup, "Vasya Vas Vus");
        var course1 = new Course("РПО", new CourseFaculty("PT"));
        _isuExtraService.CreateCourse(course1);
        var stream1 = new StudyStream(1, course1);
        var group1 = new ElectiveCourseGroup(new ElectiveCourseGroupName("РПО", 1, 2), schedule1, course1, stream1);

        _isuExtraService.AddStudentInCourse(student, course);
        _isuExtraService.AddStudentInCourse(student1, course);
        _isuExtraService.AddStudentInCourse(student1, course1);
        Assert.Contains(_isuExtraService.StudentsWithoutTwoCourses(isuGroup), st => st.Name == student.Name);
        Assert.DoesNotContain(_isuExtraService.StudentsWithoutTwoCourses(isuGroup), st => st.Name == student1.Name);
    }
}
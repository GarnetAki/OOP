using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    Course CreateCourse(Course course);

    void AddStudentInCourse(Student student, Course course);

    void LeaveStudentFromCourse(Student student, Course course);

    IReadOnlyList<StudyStream> StreamsOfCourse(Course course);

    List<Student> StudentsFromCourse(Course course);

    List<Student> StudentsWithoutTwoCourses(Group group);

    void UpdateSchedule(Schedule.ScheduleBuilder builder, ElectiveCourseGroup group);

    void UpdateScheduleIsu(Schedule.ScheduleBuilder builder, IsuGroupProector isuGroup, Group group);
}
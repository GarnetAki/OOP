using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Schedule
{
    private List<Lesson> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson>();
    }

    private Schedule(List<Lesson> lessons)
    {
        _lessons = lessons ?? throw new ArgumentNullException();
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public IReadOnlyList<Lesson> Lessons => _lessons;

    public bool CheckСompatibility(Schedule other)
    {
        return _lessons.All(lesson => !other._lessons.Any(lessonOther => lesson.ClassTime.Compare(lessonOther.ClassTime)));
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _dataLessons;

        public ScheduleBuilder()
        {
            _dataLessons = new List<Lesson>();
        }

        public ScheduleBuilder(List<Lesson> dataLessons)
        {
            _dataLessons = dataLessons ?? throw new ArgumentNullException();
        }

        public void AddData(Lesson data)
        {
            if (data == null)
                throw new ArgumentNullException();

            if (_dataLessons.Any(existClass => existClass.ClassTime.Compare(data.ClassTime)))
                throw ScheduleException.CreateLessonOnThisTimeAlreadyExist();
            _dataLessons.Add(data);
        }

        public Schedule Build()
        {
            return new Schedule(_dataLessons);
        }
    }
}
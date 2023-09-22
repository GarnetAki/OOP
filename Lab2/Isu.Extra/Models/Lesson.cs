namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(Audience audience, ProfessorId professorId, LessonTime classTime)
    {
        Audience = audience;
        ProfessorId = professorId;
        ClassTime = classTime;
    }

    public Audience Audience { get; }

    public ProfessorId ProfessorId { get; }

    public LessonTime ClassTime { get; }
}
using Isu.Models;

namespace Isu.Extra.Models;

public class Professor
{
    public Professor(ProfessorId id, StudentName fullname)
    {
        Id = id;
        Name = fullname;
    }

    public StudentName Name { get; }

    public ProfessorId Id { get; }
}
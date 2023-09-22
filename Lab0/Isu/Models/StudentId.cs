using Isu.Entities;
using Isu.Services;

namespace Isu.Models;

public class StudentId
{
    public const int MinStudentId = 100000;
    public const int MaxStudentId = 999999;
    public StudentId(int id)
    {
        ValidateId(id);
        Id = id;
    }

    public int Id { get; }

    private static void ValidateId(int id)
    {
        if (id is < MinStudentId or > MaxStudentId)
            StudentIdException.Throw(id);
    }
}
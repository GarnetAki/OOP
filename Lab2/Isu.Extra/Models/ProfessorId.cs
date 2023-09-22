using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Models;

public class ProfessorId
{
    public const int MinProfessorId = 100000;
    public const int MaxProfessorId = 999999;
    public ProfessorId(int id)
    {
        ValidateId(id);
        Id = id;
    }

    public int Id { get; }

    private static void ValidateId(int id)
    {
        if (id is < MinProfessorId or > MaxProfessorId)
            throw ProfessorIdException.CreateProfessorIdIsIncorrect(id);
    }
}
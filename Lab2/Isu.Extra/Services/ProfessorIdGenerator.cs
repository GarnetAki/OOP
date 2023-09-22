using Isu.Extra.Models;

namespace Isu.Extra.Services;

public class ProfessorIdGenerator
{
    private int _nextId;
    public ProfessorIdGenerator()
    {
        _nextId = 99999;
    }

    public ProfessorId NextId()
    {
        _nextId++;
        var id = new ProfessorId(_nextId);
        return id;
    }
}
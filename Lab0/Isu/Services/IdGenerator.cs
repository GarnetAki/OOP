using Isu.Models;

namespace Isu.Services;

public class IdGenerator
{
    private int _nextId;
    public IdGenerator()
    {
        _nextId = 99999;
    }

    public StudentId NextId()
    {
        _nextId++;
        var id = new StudentId(_nextId);
        return id;
    }
}
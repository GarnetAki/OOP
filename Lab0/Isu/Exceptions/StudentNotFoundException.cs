using Isu.Models;

namespace Isu.Services;

public class StudentNotFoundException : GroupExceptions
{
    private StudentNotFoundException(string message)
        : base(message)
    {
    }

    public static void Throw(int id, GroupName groupName)
    {
        throw new StudentNotFoundException($"Student \"{groupName}\" not found in group \"{groupName}\".");
    }
}
using Isu.Models;

namespace Isu.Services;

public class GroupIsFullException : GroupExceptions
{
    private GroupIsFullException(string message)
        : base(message)
    {
    }

    public static void Throw(GroupName groupName)
    {
        throw new GroupIsFullException($"Group {groupName} is full.");
    }
}
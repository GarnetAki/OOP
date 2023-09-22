using Isu.Models;

namespace Isu.Services;

public class GroupAlreadyExistException : GroupExceptions
{
    private GroupAlreadyExistException(string message)
        : base(message)
    {
    }

    public static void Throw(GroupName groupName)
    {
        throw new GroupAlreadyExistException($"Group {groupName} is already exist.");
    }
}
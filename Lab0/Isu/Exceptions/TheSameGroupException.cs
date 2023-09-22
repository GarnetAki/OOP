using Isu.Models;

namespace Isu.Services;

public class TheSameGroupException : GroupExceptions
{
    private TheSameGroupException(string message)
        : base(message)
    {
    }

    public static void Throw(GroupName groupName)
    {
        throw new TheSameGroupException($"Student already in group {groupName}.");
    }
}
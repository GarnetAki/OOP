using Isu.Services;

namespace Isu.Models;

public class GroupNumber
{
    public GroupNumber(string name)
    {
        ValidateGroupNumber(name);
        Number = name.Substring(1,  4);
    }

    public string Number { get; }

    private static void ValidateGroupNumber(string name)
    {
        if (name.Substring(1, 5).Any(chr => chr is > '9' or < '0'))
            GroupNameFormatException.ThrowFormat(name);
    }
}
using Isu.Services;

namespace Isu.Models;

public class GroupFaculty
{
    public GroupFaculty(char faculty)
    {
        ValidateFaculty(faculty);
        Faculty = faculty;
    }

    public char Faculty { get; }

    private static void ValidateFaculty(char faculty)
    {
        if (faculty is < 'A' or > 'Z')
            GroupNameFormatException.ThrowFaculty(faculty);
    }
}
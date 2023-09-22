using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class CourseFaculty
{
    public CourseFaculty(string faculty)
    {
        ValidateFaculty(faculty);
        Faculty = faculty;
    }

    public string Faculty { get; }

    private void ValidateFaculty(string faculty)
    {
        if (faculty != "ICT and IIDP" && faculty != "PT" && faculty != "BTLS" && faculty != "CTM" && faculty != "TMI" &&
            faculty != "TInT")
            throw CourseFacultyException.CreateFacultyDoesNotExist(faculty);
    }
}
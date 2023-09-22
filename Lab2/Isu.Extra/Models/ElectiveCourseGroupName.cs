using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class ElectiveCourseGroupName
{
    public ElectiveCourseGroupName(string name, int stream, int number)
    {
        ValidateCGroupName(stream, number);
        Name = name;
        Stream = stream;
        Number = number;
    }

    public string Name { get; }

    public int Stream { get; }

    public int Number { get; }

    public override string ToString()
    {
        return Name + Stream + "." + Number;
    }

    private void ValidateCGroupName(int stream, int number)
    {
        if (number < 0 || stream < 0)
            throw ElectiveCourseGroupNameException.CreateNameIsIncorrect();
    }
}
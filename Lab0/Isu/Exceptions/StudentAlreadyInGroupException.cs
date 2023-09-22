namespace Isu.Services;

public class StudentAlreadyInGroupException : StudentExceptions
{
    private StudentAlreadyInGroupException(string message)
        : base(message)
    {
    }

    public static void Throw(int id)
    {
        throw new StudentAlreadyInGroupException($"Student \"{id}\" is already in group.");
    }
}
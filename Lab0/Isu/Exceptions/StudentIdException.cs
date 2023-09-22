using Isu.Models;

namespace Isu.Services;

public class StudentIdException : StudentExceptions
{
    private StudentIdException(string message)
        : base(message)
    {
    }

    public static void Throw(int id)
    {
        throw new StudentIdException($"Id {id} is not correct.");
    }
}
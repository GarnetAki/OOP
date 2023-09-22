namespace Isu.Extra.Exceptions;

public class ProfessorIdException : ServiceException
{
    private ProfessorIdException(string message)
        : base(message)
    {
    }

    public static ProfessorIdException CreateProfessorIdIsIncorrect(int id)
        => new ProfessorIdException($"Professor Id {id} is incorrect.");
}
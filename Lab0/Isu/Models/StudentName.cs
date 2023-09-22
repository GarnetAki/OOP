using Isu.Services;

namespace Isu.Models;

public class StudentName
{
    public StudentName(string fullname)
    {
        ValidateName(fullname);
        string[] substr = fullname.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);

        FirstName = substr[1];
        Patronymic = substr[2];
        SecondName = substr[0];
    }

    public string SecondName { get; }

    public string FirstName { get; }

    public string Patronymic { get; }

    public string NameToString()
    {
        return SecondName + " " + FirstName + " " + Patronymic;
    }

    private static void ValidateName(string fullname)
    {
        if (fullname == null)
            throw new ArgumentNullException();
        string[] subs = fullname.Split(' ');
        if (subs.Length != 3)
            NameIsNotCorrectException.ThrowFormat();
        else if (subs.Count(substr => (substr[0] >= 'A' && substr[0] <= 'Z') && substr.Count(chr => chr is >= 'a' and <= 'z') == substr.Length - 1) != 3)
            NameIsNotCorrectException.ThrowChar();
    }
}
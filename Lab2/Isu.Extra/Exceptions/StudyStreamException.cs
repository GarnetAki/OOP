namespace Isu.Extra.Exceptions;

public class StudyStreamException : IsuServiceException
{
    private StudyStreamException(string message)
        : base(message)
    {
    }

    public static StudyStreamException CreateStudyStreamsIsEmpty()
        => new StudyStreamException($"Study streams is empty.");

    public static StudyStreamException CreateStudyStreamNumbersRepeats()
        => new StudyStreamException($"Study stream numbers repeats.");
}
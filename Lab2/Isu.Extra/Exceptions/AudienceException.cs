namespace Isu.Extra.Exceptions;

public class AudienceException : IsuServiceException
{
    private AudienceException(string message)
        : base(message)
    {
    }

    public static AudienceException CreateAudienceNumberIncorrect()
        => new AudienceException($"Audience number is incorrect.");
}
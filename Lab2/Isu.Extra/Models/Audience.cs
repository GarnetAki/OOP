using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Audience
{
    public Audience(Address address, int audienceNumber)
    {
        if (address == null)
            throw new ArgumentNullException();

        ValidateAudience(audienceNumber);
        Address = address;
        AudienceNumber = audienceNumber;
    }

    public Address Address { get; }

    public int AudienceNumber { get; }

    private void ValidateAudience(int audienceNumber)
    {
        if (audienceNumber < 100)
            throw AudienceException.CreateAudienceNumberIncorrect();
    }
}
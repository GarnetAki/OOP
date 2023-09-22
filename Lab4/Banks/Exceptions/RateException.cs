namespace Banks.Exceptions;

public class RateException : Exception
{
    private RateException(string message)
        : base(message)
    {
    }

    public static RateException CreateInvalidLimit()
        => new RateException($"Limit is invalid.");

    public static RateException CreateInvalidCommission()
        => new RateException($"Commission is invalid.");

    public static RateException CreateInvalidPercent()
        => new RateException($"Percent is invalid.");

    public static RateException CreateInvalidPercentIntervals()
        => new RateException($"Percent intervals are invalid.");

    public static RateException CreateInvalidTimeInterval()
        => new RateException($"Time interval are invalid.");
}
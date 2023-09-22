using Banks.Exceptions;

namespace Banks.Entities;

public class DepositInformation
{
    private List<DepositInformationPart> _parts;

    public DepositInformation(List<DepositInformationPart> parts, TimeSpan span)
    {
        ValidateDepositInformation(parts, span);
        _parts = parts;
        Span = span;
    }

    public IReadOnlyList<DepositInformationPart> Parts => _parts;

    public TimeSpan Span { get; }

    public decimal Percent(decimal balance)
    {
        for (int index = 0; index < _parts.Count; index++)
        {
            if (_parts[index].MaximalSum > balance ||
                _parts[index].MaximalSum == -1)
                return _parts[index].Percent;
        }

        throw RateException.CreateInvalidPercentIntervals();
    }

    public string Show()
    {
        string information = $"[Time to withdraw option: {Span.Days}]\n";
        for (int index = 0; index < _parts.Count; index++)
        {
            DepositInformationPart part = _parts[index];
            information += $"[{part.MinimalSum} to {part.MaximalSum} percent is {part.Percent}]\n";
        }

        return information;
    }

    private static void ValidateDepositInformation(List<DepositInformationPart> parts, TimeSpan span)
    {
        if (span <= TimeSpan.Zero)
            throw RateException.CreateInvalidTimeInterval();

        decimal tmp = 0;
        for (int index = 0; index < parts.Count; index++)
        {
            DepositInformationPart part = parts[index];
            if (part.Percent < 0)
                throw RateException.CreateInvalidPercent();

            if (tmp != part.MinimalSum)
                throw RateException.CreateInvalidPercentIntervals();

            if (part.MinimalSum >= part.MaximalSum && part.MaximalSum != -1)
                throw RateException.CreateInvalidPercentIntervals();

            tmp = part.MaximalSum;
        }

        if (tmp != -1)
            throw RateException.CreateInvalidPercentIntervals();
    }
}
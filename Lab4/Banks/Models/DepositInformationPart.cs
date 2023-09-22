namespace Banks.Entities;

public class DepositInformationPart
{
    public DepositInformationPart(decimal minimalSum, decimal maximumSum, decimal percent)
    {
        MinimalSum = minimalSum;
        MaximalSum = maximumSum;
        Percent = percent;
    }

    public decimal MinimalSum { get; }

    public decimal MaximalSum { get; }

    public decimal Percent { get; }
}
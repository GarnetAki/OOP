using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class Rate : IRate
{
    public Rate(decimal limit, decimal debitPercent, decimal commission, DepositInformation depositPercentInformation)
    {
        VerifiedCommission(commission);
        VerifiedPercent(debitPercent);
        VerifiedLimit(limit);

        Limit = limit;
        DebitPercent = debitPercent;
        Commission = commission;
        DepositPercentInformation = depositPercentInformation;
    }

    public decimal Limit { get; private set; }

    public decimal DebitPercent { get; private set; }

    public decimal Commission { get; private set; }

    public DepositInformation DepositPercentInformation { get; private set; }

    public void ChangeCommission(decimal newCommission)
    {
        VerifiedCommission(newCommission);
        Commission = newCommission;
    }

    public void ChangeDebitPercent(decimal newPercent)
    {
        VerifiedPercent(newPercent);
        DebitPercent = newPercent;
    }

    public void ChangeDepositInformation(DepositInformation newInformation)
    {
        DepositPercentInformation = newInformation;
    }

    public void ChangeLimit(decimal newLimit)
    {
        VerifiedLimit(newLimit);
        Limit = newLimit;
    }

    private static void VerifiedLimit(decimal limit)
    {
        if (limit < 0)
            throw RateException.CreateInvalidLimit();
    }

    private static void VerifiedCommission(decimal commission)
    {
        if (commission < 0)
            throw RateException.CreateInvalidCommission();
    }

    private static void VerifiedPercent(decimal percent)
    {
        if (percent < 0)
            throw RateException.CreateInvalidPercent();
    }
}
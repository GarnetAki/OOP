namespace Banks.Entities;

public interface IRate
{
    decimal Limit { get; }

    decimal DebitPercent { get; }

    decimal Commission { get; }

    DepositInformation DepositPercentInformation { get; }

    void ChangeDepositInformation(DepositInformation newInformation);

    void ChangeDebitPercent(decimal newPercent);

    void ChangeCommission(decimal newCommission);

    void ChangeLimit(decimal newLimit);
}
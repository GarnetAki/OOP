using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class CreditAccount : IAccount
{
    public CreditAccount(Guid accountId, IRate rate)
    {
        Balance = 0;
        Commission = rate.Commission;
        Id = accountId;
    }

    public Guid Id { get; }

    public decimal Balance { get; private set; }

    public decimal Commission { get; private set; }

    public decimal CheckFutureBalance(TimeSpan timeSpan)
    {
        return Balance;
    }

    public void ChangeRate(IRate newRate)
    {
        Commission = newRate.Commission;
    }

    public void Update(IClock clock)
    {
    }

    public decimal AddMoney(decimal money)
    {
        VerifiedOperations.VerifiedMoneyCount(money);

        Balance += money;
        return money;
    }

    public decimal ReduceMoney(decimal money, bool verified, decimal limit)
    {
        VerifiedOperations.VerifiedMoneyCount(money);
        VerifiedOperations.VerifiedAccountLimit(verified, money, limit);

        if (Balance < 0)
            money += Commission;

        Balance -= money;
        return money;
    }
}
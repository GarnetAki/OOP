using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class DebitAccount : IAccount
{
    private readonly DateTime _creationTime;
    private DateTime _lastUpdate;

    public DebitAccount(Guid accountId, IRate rate, IClock clock)
    {
        _creationTime = clock.VirtualDate;
        _lastUpdate = clock.VirtualDate;
        Balance = 0;
        MemorizedMoney = 0;
        Id = accountId;
        Percent = rate.DebitPercent;
        clock.Attach(this);
    }

    public Guid Id { get; }

    public decimal Balance { get; private set; }

    public decimal MemorizedMoney { get; private set; }

    public decimal Percent { get; private set; }

    public void ChangeRate(IRate newRate)
    {
        Percent = newRate.DebitPercent;
    }

    public void Update(IClock clock)
    {
        _lastUpdate = clock.VirtualDate;
        MemorizeMoney();

        if (clock.VirtualDate.Day == 1 && (clock.VirtualDate.Month > _creationTime.Month || clock.VirtualDate.Year > _creationTime.Year))
            GiveMemorizedMoney();
    }

    public decimal CheckFutureBalance(TimeSpan timeSpan)
    {
        decimal addictionPart = 0;
        decimal futureBalance = Balance;
        DateTime futureTime = _lastUpdate;
        while (timeSpan.Days > 0)
        {
            futureTime = futureTime.AddDays(1);
            timeSpan = timeSpan.Subtract(TimeSpan.FromDays(1));

            addictionPart += ((Percent / 365M) + 1) * futureBalance;

            if (futureTime.Day == 1 &&
                (futureTime.Month > _creationTime.Month || futureTime.Year > _creationTime.Year))
            {
                futureBalance += addictionPart;
                addictionPart = 0;
            }
        }

        return futureBalance;
    }

    public void MemorizeMoney()
    {
        MemorizedMoney += ((Percent / 365) + 1) * Balance;
    }

    public void GiveMemorizedMoney()
    {
        Balance += MemorizedMoney;
        MemorizedMoney = 0;
    }

    public decimal AddMoney(decimal money)
    {
        VerifiedOperations.VerifiedMoneyCount(money);

        Balance += money;
        return money;
    }

    public decimal ReduceMoney(decimal money, bool verified, decimal limit)
    {
        VerifiedOperations.VerifiedAccountLimit(verified, money, limit);
        VerifiedOperations.VerifiedBalance(Balance, money);
        VerifiedOperations.VerifiedMoneyCount(money);

        Balance -= money;
        return money;
    }
}
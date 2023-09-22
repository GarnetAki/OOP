namespace Banks.Entities;

public interface IAccount
{
    Guid Id { get; }

    decimal Balance { get; }

    decimal CheckFutureBalance(TimeSpan timeSpan);

    void Update(IClock clock);

    void ChangeRate(IRate newRate);

    decimal AddMoney(decimal money);

    decimal ReduceMoney(decimal money, bool verified, decimal limit);
}
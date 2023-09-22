using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Services;

public class AccountCreator : IAccountCreator
{
    public IAccount Create(string type, IRate rate, IClock clock)
    {
        var newId = Guid.NewGuid();

        return type switch
        {
            "Credit" => new CreditAccount(newId, rate),
            "Debit" => new DebitAccount(newId, rate, clock),
            "Deposit" => new DepositAccount(newId, rate, clock),
            _ => throw AccountException.CreateInvalidAccountType()
        };
    }
}
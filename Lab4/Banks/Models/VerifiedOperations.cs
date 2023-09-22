using System.Linq.Expressions;
using Banks.Exceptions;

namespace Banks.Models;

public class VerifiedOperations
{
    public static void VerifiedMoneyCount(decimal money)
    {
        if (money <= 0)
            throw AccountException.CreateInvalidMoneyCount();
    }

    public static void VerifiedFreezeTime(bool canReduce)
    {
        if (!canReduce)
            throw AccountException.CreateWithdrawIsNotPossible();
    }

    public static void VerifiedAccountLimit(bool verified, decimal money, decimal limit)
    {
        if (!verified && money > limit)
            throw BankException.CreateOperationExceedsLimit();
    }

    public static void VerifiedBalance(decimal balance, decimal money)
    {
        if (money > balance)
            throw BankException.CreateNotEnoughMoney();
    }
}
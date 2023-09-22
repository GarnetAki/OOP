namespace Banks.Exceptions;

public class AccountException : Exception
{
    private AccountException(string message)
        : base(message)
    {
    }

    public static AccountException CreateInvalidAccountType()
        => new AccountException($"Account type is invalid.");

    public static AccountException CreateInvalidMoneyCount()
        => new AccountException($"Money count must be positive.");

    public static AccountException CreateWithdrawIsNotPossible()
        => new AccountException($"Withdraw period is not came yet.");
}
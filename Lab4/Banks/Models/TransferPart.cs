namespace Banks.Models;

public class TransferPart
{
    public TransferPart(Guid bankId, Guid accountId, decimal sum, bool isInput)
    {
        BankId = bankId;
        AccountId = accountId;
        Sum = sum;
        IsInput = isInput;
    }

    public Guid BankId { get; }

    public Guid AccountId { get; }

    public decimal Sum { get; }

    public bool IsInput { get; }
}
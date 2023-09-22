using Banks.Entities;
using Banks.Models;

namespace Banks.Services;

public interface ICentralBank
{
    IReadOnlyList<Bank> Banks { get; }

    IClock Clock { get; }

    IReadOnlyList<Transfer> Transfers { get; }

    Guid AddBank(IRate rate);

    decimal CheckFutureBalance(TimeSpan timeSpan, Guid bankId, Guid accountId);

    Guid AddClient(Guid bankId, IClient client);

    Guid AddAccount(Guid bankId, Guid clientId, string type);

    void DepositMoney(Guid bankId, Guid accountId, decimal sum);

    void WithdrawMoney(Guid bankId, Guid accountId, decimal sum);

    void ChangeDebitPercent(Guid bankId, decimal percent);

    void ChangeCommission(Guid bankId, decimal commission);

    void ChangeDepositInformation(Guid bankId, DepositInformation depositInformation);

    void ChangeLimit(Guid bankId, decimal limit);

    IClient GetClient(Guid id);

    Bank GetBank(Guid bankId);

    void CancelTransfer(Guid id);

    void TransferMoney(decimal sum, Guid firstBankId, Guid firstAccount, Guid secondBankId, Guid secondAccount);
}
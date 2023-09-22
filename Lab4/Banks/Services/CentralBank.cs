using Banks.Entities;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private List<Bank> _banks;
    private IAccountCreator _creator;
    private List<Transfer> _transfers;

    public CentralBank(IAccountCreator creator)
    {
        _transfers = new List<Transfer>();
        Clock = new Clock();
        _creator = creator;
        _banks = new List<Bank>();
    }

    public IReadOnlyList<Bank> Banks => _banks;

    public IClock Clock { get; }

    public IReadOnlyList<Transfer> Transfers => _transfers;

    public Guid AddBank(IRate rate)
    {
        var newGuid = Guid.NewGuid();
        _banks.Add(new Bank(newGuid, rate));
        return newGuid;
    }

    public decimal CheckFutureBalance(TimeSpan timeSpan, Guid bankId, Guid accountId)
    {
        return GetBank(bankId).GetAccount(accountId).CheckFutureBalance(timeSpan);
    }

    public void AddMoney(decimal sum, Guid bankId, Guid accountId)
    {
        _transfers.Add(new Transfer(Guid.NewGuid(), new List<TransferPart> { GetBank(bankId).AddMoney(accountId, sum) }));
    }

    public void ReduceMoney(decimal sum, Guid bankId, Guid accountId)
    {
        _transfers.Add(new Transfer(Guid.NewGuid(), new List<TransferPart> { GetBank(bankId).ReduceMoney(accountId, sum) }));
    }

    public Guid AddClient(Guid bankId, IClient client)
    {
        return GetBank(bankId).RegisterClient(client);
    }

    public Guid AddAccount(Guid bankId, Guid clientId, string type)
    {
        return GetBank(bankId).AddAccount(clientId, type, Clock, _creator);
    }

    public void ChangeDebitPercent(Guid bankId, decimal percent)
    {
        GetBank(bankId).ChangeDebitPercent(percent);
    }

    public void ChangeCommission(Guid bankId, decimal commission)
    {
        GetBank(bankId).ChangeCommission(commission);
    }

    public void ChangeDepositInformation(Guid bankId, DepositInformation depositInformation)
    {
        GetBank(bankId).ChangeDepositInformation(depositInformation);
    }

    public void ChangeLimit(Guid bankId, decimal limit)
    {
        GetBank(bankId).ChangeLimit(limit);
    }

    public void DepositMoney(Guid bankId, Guid accountId, decimal sum)
    {
        _transfers.Add(new Transfer(Guid.NewGuid(), new List<TransferPart>
            { GetBank(bankId).AddMoney(accountId, sum) }));
    }

    public void WithdrawMoney(Guid bankId, Guid accountId, decimal sum)
    {
        _transfers.Add(new Transfer(Guid.NewGuid(), new List<TransferPart>
            { GetBank(bankId).ReduceMoney(accountId, sum) }));
    }

    public IClient GetClient(Guid id)
    {
        return _banks.First(bank => bank.Clients.FirstOrDefault(client => client.Id == id) != null)
            .Clients.First(client => client.Id == id);
    }

    public Bank GetBank(Guid bankId)
    {
        return _banks.First(bank => bank.Id == bankId);
    }

    public void CancelTransfer(Guid id)
    {
        foreach (TransferPart part in _transfers.First(transfer => transfer.Id == id).Parts)
        {
            if (part.IsInput)
            {
                GetBank(part.BankId).AbsoluteReduceMoney(part.AccountId, part.Sum);
            }
            else
            {
                GetBank(part.BankId).AddMoney(part.AccountId, part.Sum);
            }
        }

        _transfers.Remove(_transfers.First(transfer => transfer.Id == id));
    }

    public void TransferMoney(decimal sum, Guid firstBankId, Guid firstAccount, Guid secondBankId, Guid secondAccount)
    {
        GetBank(secondBankId).GetAccount(secondAccount);
        _transfers.Add(new Transfer(Guid.NewGuid(), new List<TransferPart>
            { GetBank(firstBankId).AddMoney(firstAccount, sum),  GetBank(secondBankId).AddMoney(secondAccount, sum) }));
    }
}
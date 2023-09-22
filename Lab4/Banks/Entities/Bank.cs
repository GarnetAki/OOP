using Banks.Exceptions;
using Banks.Models;
using Banks.Services;

namespace Banks.Entities;

public class Bank
{
    private List<IClient> _clients;

    public Bank(Guid bankId, IRate rate)
    {
        _clients = new List<IClient>();
        Id = bankId;
        Rate = rate;
    }

    public Guid Id { get; }

    public IRate Rate { get; }

    public IReadOnlyList<IClient> Clients => _clients;

    public Guid AddAccount(Guid clientId, string type, IClock clock, IAccountCreator accountCreator)
    {
        return _clients.Single(client => client.Id == clientId).AddAccount(accountCreator.Create(type, Rate, clock));
    }

    public IAccount GetAccount(Guid id)
    {
        return _clients.Single(client => client.Accounts.FirstOrDefault(account => account.Id == id) != null).Accounts
            .Single(account => account.Id == id);
    }

    public TransferPart AddMoney(Guid accountId, decimal sum)
    {
        decimal finalSum = GetAccount(accountId).AddMoney(sum);
        return new TransferPart(Id, accountId, finalSum, true);
    }

    public TransferPart ReduceMoney(Guid accountId, decimal sum)
    {
        IClient client = _clients.Single(client => client.Accounts.FirstOrDefault(account => account.Id == accountId) != null);
        if (!client.IsVerified && sum > Rate.Limit)
            throw BankException.CreateOperationExceedsLimit();

        IAccount account = GetAccount(accountId);
        if (account is not CreditAccount && account.Balance < sum)
            throw BankException.CreateNotEnoughMoney();

        decimal finalSum = account.ReduceMoney(sum, client.IsVerified, Rate.Limit);
        return new TransferPart(Id, accountId, finalSum, false);
    }

    public void AbsoluteReduceMoney(Guid accountId, decimal sum)
    {
        IAccount account = GetAccount(accountId);
        account.ReduceMoney(sum, true, 0);
    }

    public Guid RegisterClient(IClient client)
    {
        if (_clients.Contains(client))
            throw BankException.CreateClientAlreadyExists();

        _clients.Add(client);
        return client.Id;
    }

    public void ChangeLimit(decimal newLimit)
    {
        Rate.ChangeLimit(newLimit);
    }

    public void ChangeCommission(decimal newCommission)
    {
        decimal oldCommission = Rate.Commission;
        Rate.ChangeCommission(newCommission);
        foreach (IClient client in _clients.FindAll(client => client.Accounts.Any(account => account is CreditAccount)))
        {
            if (client.IsSubscribed)
                client.Email!.SendMail($"Credit commission has been changed. Old commission: {oldCommission}, new commission: {newCommission}");
        }

        foreach (IAccount account in _clients.SelectMany(client => client.Accounts))
        {
            account.ChangeRate(Rate);
        }
    }

    public void ChangeDebitPercent(decimal newPercent)
    {
        decimal oldPercent = Rate.DebitPercent;
        Rate.ChangeDebitPercent(newPercent);
        foreach (IClient client in _clients.FindAll(client => client.Accounts.Any(account => account is DebitAccount)))
        {
            if (client.IsSubscribed)
                client.Email!.SendMail($"Debit percent has been changed. Old percent: {oldPercent}, new percent: {newPercent}");
        }

        foreach (IAccount account in _clients.SelectMany(client => client.Accounts))
        {
            account.ChangeRate(Rate);
        }
    }

    public void ChangeDepositInformation(DepositInformation newInformation)
    {
        DepositInformation oldInformation = Rate.DepositPercentInformation;
        Rate.ChangeDepositInformation(newInformation);
        foreach (IClient client in _clients.FindAll(client => client.Accounts.Any(account => account is DepositAccount)))
        {
            if (client.IsSubscribed)
                client.Email!.SendMail($"Deposit information has been changed. Old percent: {oldInformation.Show()}, new percent: {newInformation.Show()}");
        }

        foreach (IAccount account in _clients.SelectMany(client => client.Accounts))
        {
            account.ChangeRate(Rate);
        }
    }
}
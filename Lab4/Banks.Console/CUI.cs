using System.Security.Principal;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;

public class CUI : IU
{
    private static IAccountCreator _creator = new AccountCreator();
    private string? _inputString = string.Empty;
    private ICentralBank _centralBank = new CentralBank(_creator);

    public void Run()
    {
        while (_inputString != "Stop")
        {
            _inputString = System.Console.ReadLine();
            Parse();
        }
    }

    private void Parse()
    {
        switch (_inputString)
        {
            case "Create bank":
                CreateBank();
                break;
            case "Create client":
                CreateClient();
                break;
            case "Create account":
                CreateAccount();
                break;
            case "Change debit percent":
                ChangeDebitPercent();
                break;
            case "Change commission":
                ChangeCommission();
                break;
            case "Change deposit information":
                ChangeDepositInformation();
                break;
            case "Change limit":
                ChangeLimit();
                break;
            case "Deposit money":
                DepositMoney();
                break;
            case "Withdraw money":
                WithdrawMoney();
                break;
            case "Transfer money":
                TransferMoney();
                break;
            case "Show transfer history":
                ShowTransferHistory();
                break;
            case "Cancel transfer":
                CancelTransfer();
                break;
            case "Check future balance":
                CheckFutureBalance();
                break;
            case "Show banks":
                ShowBanks();
                break;
            case "Show clients":
                ShowClients();
                break;
            case "Show client accounts":
                ShowClientAccounts();
                break;
            case "Init or change client passport":
                InitOrChangeClientPassport();
                break;
            case "Init or change client address":
                InitOrChangeClientAddress();
                break;
            case "Init or change client email":
                InitOrChangeClientEmail();
                break;
            case "Change client firstname":
                ChangeClientFirstname();
                break;
            case "Change client lastname":
                ChangeClientLastname();
                break;
            case "Next day":
                NextDay();
                break;
        }
    }

    private void CreateBank()
    {
        System.Console.WriteLine("Enter commission:");
        string? commission = System.Console.ReadLine();
        System.Console.WriteLine("Enter deposit percent:");
        string? percent = System.Console.ReadLine();
        System.Console.WriteLine("Enter limit:");
        string? limit = System.Console.ReadLine();
        System.Console.WriteLine("Enter time of deposit account withdraw block:");
        string? span = System.Console.ReadLine();
        System.Console.WriteLine("Enter count of deposit parts:");
        string? count = System.Console.ReadLine();
        if (commission == null || percent == null || limit == null || count == null || span == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        var list = new List<DepositInformationPart>();
        for (int i = 0; i < int.Parse(count); i++)
        {
            System.Console.WriteLine("Enter minimal sum of deposit part:");
            string? minSum = System.Console.ReadLine();
            System.Console.WriteLine("Enter maximal sum of deposit part:");
            string? maxSum = System.Console.ReadLine();
            System.Console.WriteLine("Enter percent of deposit part:");
            string? depositPercent = System.Console.ReadLine();
            if (minSum == null || maxSum == null || depositPercent == null)
            {
                System.Console.WriteLine("You entered empty string");
                return;
            }

            list.Add(new DepositInformationPart(decimal.Parse(minSum), decimal.Parse(maxSum), decimal.Parse(depositPercent)));
        }

        try
        {
            System.Console.WriteLine($"Bank created, id: {_centralBank.AddBank(new Rate(decimal.Parse(limit), decimal.Parse(percent), decimal.Parse(commission), new DepositInformation(list, TimeSpan.Parse(span))))}");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void CreateClient()
    {
        System.Console.WriteLine("Enter id of bank:");
        string? id = System.Console.ReadLine();
        System.Console.WriteLine("Enter firstname:");
        string? firstname = System.Console.ReadLine();
        System.Console.WriteLine("Enter lastname:");
        string? lastname = System.Console.ReadLine();
        System.Console.WriteLine("Enter email:");
        string? email = System.Console.ReadLine();
        System.Console.WriteLine("Enter address:");
        string? address = System.Console.ReadLine();
        System.Console.WriteLine("Enter passport series:");
        string? series = System.Console.ReadLine();
        System.Console.WriteLine("Enter passport number:");
        string? number = System.Console.ReadLine();
        if (firstname == null || lastname == null || id == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        Client.ClientBuilder newClient = Client.Builder();
        try
        {
            newClient.WithFirstname(firstname);
            newClient.WithLastname(lastname);
            if (!string.IsNullOrEmpty(address))
                newClient.WithAddress(new Address(address));

            if (!string.IsNullOrEmpty(series) && !string.IsNullOrEmpty(number))
                newClient.WithPassport(new Passport(series, number));

            if (!string.IsNullOrEmpty(email))
                newClient.WithEmail(new Email(email));

            System.Console.WriteLine($"Client created, id: {_centralBank.AddClient(Guid.Parse(id), newClient.Build())}");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (BankException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void CreateAccount()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter client id:");
        string? clientId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account type:");
        string? type = System.Console.ReadLine();
        if (bankId == null || clientId == null || type == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.AddAccount(Guid.Parse(bankId), Guid.Parse(clientId), type);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeDebitPercent()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter debit percent:");
        string? newPercent = System.Console.ReadLine();
        if (bankId == null || newPercent == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.ChangeDebitPercent(Guid.Parse(bankId), decimal.Parse(newPercent));
            System.Console.WriteLine("Debit percent changed.");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeCommission()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter commission:");
        string? commission = System.Console.ReadLine();
        if (bankId == null || commission == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.ChangeCommission(Guid.Parse(bankId), decimal.Parse(commission));
            System.Console.WriteLine("Commission changed.");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeDepositInformation()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter time of deposit account withdraw block:");
        string? span = System.Console.ReadLine();
        System.Console.WriteLine("Enter count of deposit parts:");
        string? count = System.Console.ReadLine();
        if (bankId == null || span == null || count == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        var list = new List<DepositInformationPart>();
        for (int i = 0; i < int.Parse(count); i++)
        {
            System.Console.WriteLine("Enter minimal sum of deposit part:");
            string? minSum = System.Console.ReadLine();
            System.Console.WriteLine("Enter maximal sum of deposit part:");
            string? maxSum = System.Console.ReadLine();
            System.Console.WriteLine("Enter percent of deposit part:");
            string? depositPercent = System.Console.ReadLine();
            if (minSum == null || maxSum == null || depositPercent == null)
            {
                System.Console.WriteLine("You entered empty string");
                return;
            }

            list.Add(new DepositInformationPart(decimal.Parse(minSum), decimal.Parse(maxSum), decimal.Parse(depositPercent)));
        }

        try
        {
            _centralBank.ChangeDepositInformation(Guid.Parse(bankId), new DepositInformation(list, TimeSpan.Parse(span)));
            System.Console.WriteLine("Deposit information changed.");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeLimit()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter limit:");
        string? limit = System.Console.ReadLine();
        if (bankId == null || limit == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.ChangeLimit(Guid.Parse(bankId), decimal.Parse(limit));
            System.Console.WriteLine("Limit changed.");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void DepositMoney()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account id:");
        string? accountId = System.Console.ReadLine();
        System.Console.WriteLine("Enter sum:");
        string? sum = System.Console.ReadLine();
        if (bankId == null || accountId == null || sum == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.DepositMoney(Guid.Parse(bankId), Guid.Parse(accountId), decimal.Parse(sum));
            System.Console.WriteLine("Money deposited.");
        }
        catch (AccountException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (BankException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void WithdrawMoney()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account id:");
        string? accountId = System.Console.ReadLine();
        System.Console.WriteLine("Enter sum:");
        string? sum = System.Console.ReadLine();
        if (bankId == null || accountId == null || sum == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.WithdrawMoney(Guid.Parse(bankId), Guid.Parse(accountId), decimal.Parse(sum));
            System.Console.WriteLine("Money withdrawn.");
        }
        catch (AccountException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (BankException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void TransferMoney()
    {
        System.Console.WriteLine("Enter bank id (to withdraw):");
        string? firstBankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account id (to withdraw):");
        string? firstAccountId = System.Console.ReadLine();
        System.Console.WriteLine("Enter bank id (to deposit):");
        string? secondBankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account id (to deposit):");
        string? secondAccountId = System.Console.ReadLine();
        System.Console.WriteLine("Enter sum:");
        string? sum = System.Console.ReadLine();
        if (firstBankId == null || firstAccountId == null || secondBankId == null || secondAccountId == null || sum == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.TransferMoney(decimal.Parse(sum), Guid.Parse(firstBankId), Guid.Parse(firstAccountId), Guid.Parse(secondBankId), Guid.Parse(secondAccountId));
            System.Console.WriteLine("Money transfered.");
        }
        catch (AccountException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (BankException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ShowTransferHistory()
    {
        foreach (Transfer transfer in _centralBank.Transfers)
        {
            System.Console.WriteLine(transfer.Id);
            foreach (TransferPart part in transfer.Parts)
            {
                string type = part.IsInput ? "deposit" : "withdraw";

                System.Console.WriteLine($"Account id: {part.AccountId} | sum: {part.Sum} | {type}");
            }

            System.Console.WriteLine("---");
        }
    }

    private void CancelTransfer()
    {
        System.Console.WriteLine("Enter transfer id:");
        string? id = System.Console.ReadLine();
        if (id == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.CancelTransfer(Guid.Parse(id));
            System.Console.WriteLine("Transfer canceled.");
        }
        catch (RateException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void CheckFutureBalance()
    {
        System.Console.WriteLine("Enter bank id:");
        string? bankId = System.Console.ReadLine();
        System.Console.WriteLine("Enter account id:");
        string? accountId = System.Console.ReadLine();
        System.Console.WriteLine("Enter days count:");
        string? count = System.Console.ReadLine();
        if (bankId == null || accountId == null || count == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            _centralBank.CheckFutureBalance(TimeSpan.Parse(count), Guid.Parse(bankId), Guid.Parse(accountId));
            System.Console.WriteLine("Money withdrawn.");
        }
        catch (BankException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ShowBanks()
    {
        int ind = 1;
        foreach (Bank bank in _centralBank.Banks)
        {
            System.Console.WriteLine($"{ind} | bank id: {bank.Id}");
            ind++;
        }
    }

    private void ShowClients()
    {
        System.Console.WriteLine("Enter bank id:");
        string? tmp = System.Console.ReadLine();
        if (tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            int ind = 1;
            foreach (IClient client in _centralBank.GetBank(Guid.Parse(tmp)).Clients)
            {
                System.Console.WriteLine($"{ind} | client name: {client.Firstname} {client.Lastname} | client id: {client.Id}");
                ind++;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ShowClientAccounts()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        if (tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            int ind = 1;
            foreach (IAccount account in _centralBank.GetClient(Guid.Parse(tmp)).Accounts)
            {
                System.Console.WriteLine($"{ind} | account balance: {account.Balance} | account id: {account.Id}");
                ind++;
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void InitOrChangeClientEmail()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        System.Console.WriteLine("Enter email:");
        string? email = System.Console.ReadLine();
        if (email == null || tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            var id = Guid.Parse(tmp);
            var clientEmail = new Email(email);
            _centralBank.GetClient(id).InitOrChangeEmail(clientEmail);
            System.Console.WriteLine("Email initialized or changed.");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void InitOrChangeClientAddress()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        System.Console.WriteLine("Enter address:");
        string? address = System.Console.ReadLine();
        if (address == null || tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            var id = Guid.Parse(tmp);
            var clientAddress = new Address(address);
            _centralBank.GetClient(id).InitOrChangeAddress(clientAddress);
            System.Console.WriteLine("Address initialized or changed.");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void InitOrChangeClientPassport()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        System.Console.WriteLine("Enter passport series:");
        string? passportSeries = System.Console.ReadLine();
        System.Console.WriteLine("Enter passport number:");
        string? passportNumber = System.Console.ReadLine();
        if (passportNumber == null || passportSeries == null || tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            var id = Guid.Parse(tmp);
            var passport = new Passport(passportSeries, passportNumber);
            _centralBank.GetClient(id).InitOrChangePassport(passport);
            System.Console.WriteLine("Passport initialized or changed.");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeClientFirstname()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        System.Console.WriteLine("Enter firstname:");
        string? firstname = System.Console.ReadLine();
        if (firstname == null || tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            var id = Guid.Parse(tmp);
            _centralBank.GetClient(id).ChangeFirstname(firstname);
            System.Console.WriteLine("Firstname changed.");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void ChangeClientLastname()
    {
        System.Console.WriteLine("Enter client id:");
        string? tmp = System.Console.ReadLine();
        System.Console.WriteLine("Enter lastname:");
        string? lastname = System.Console.ReadLine();
        if (lastname == null || tmp == null)
        {
            System.Console.WriteLine("You entered empty string");
            return;
        }

        try
        {
            var id = Guid.Parse(tmp);
            _centralBank.GetClient(id).ChangeLastname(lastname);
            System.Console.WriteLine("Lastname changed.");
        }
        catch (ClientException e)
        {
            System.Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }

    private void NextDay()
    {
        _centralBank.Clock.NextDay();
        System.Console.WriteLine("Day incrased.");
    }
}
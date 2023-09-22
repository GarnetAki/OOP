using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class CentralBankTest
{
    [Fact]
    public void CreateBankClientAccount()
    {
        var creator = new AccountCreator();
        var cb = new CentralBank(creator);
        var list = new List<DepositInformationPart>();
        list.Add(new DepositInformationPart(0, -1, 2));
        var id = cb.AddBank(new Rate(2, 365 / 35M, 2, new DepositInformation(list, TimeSpan.FromDays(2))));
        Client.ClientBuilder client = Client.Builder();
        client.WithFirstname("Ab");
        client.WithLastname("Abob");
        var cl = client.Build();
        var clId = cb.AddClient(id, cl);
        var accId = cb.AddAccount(id, clId, "Debit");

        Assert.Equal(cb.GetClient(clId).Accounts[0].Id, accId);

        cb.AddMoney(200, id, accId);
        Assert.Equal(200, cb.GetClient(clId).Accounts[0].Balance);
    }
}
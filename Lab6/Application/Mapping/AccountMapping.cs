using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
        => new AccountDto(account.Id, account.Users.Select(u => u.Id).ToList(), account.MessageSources.Select(ms => ms.Id).ToList());
}
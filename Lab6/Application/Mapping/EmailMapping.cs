using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class EmailMapping
{
    public static EmailDto AsDto(this Email email)
        => new EmailDto(email.Id, email.Accounts.Select(u => u.Id).ToList(), email.EmailAddress, email.Messages.Select(u => u.Id).ToList());
}
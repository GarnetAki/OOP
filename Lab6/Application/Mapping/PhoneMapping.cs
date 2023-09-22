using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class PhoneMapping
{
    public static PhoneDto AsDto(this Phone phone)
        => new PhoneDto(phone.Id, phone.Accounts.Select(u => u.Id).ToList(), phone.PhoneNumber, phone.Messages.Select(u => u.Id).ToList());
}
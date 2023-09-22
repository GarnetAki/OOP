using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class MessageSourceMapping
{
    public static MessageSourceDto AsDto(this MessageSource source)
        => new MessageSourceDto(source.Id, source.Accounts.Select(u => u.Id).ToList(), source.Messages.Select(u => u.Id).ToList());
}
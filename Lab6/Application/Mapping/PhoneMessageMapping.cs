using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class PhoneMessageMapping
{
    public static PhoneMessageDto AsDto(this PhoneMessage message)
        => new PhoneMessageDto(message.Id, message.MessageSource.Id, message.Status, message.Text, message.Sender);
}
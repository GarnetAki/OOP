using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class EmailMessageMapping
{
    public static EmailMessageDto AsDto(this EmailMessage message)
        => new EmailMessageDto(message.Id, message.MessageSource.Id, message.Status, message.Title, message.Text, message.Sender);
}
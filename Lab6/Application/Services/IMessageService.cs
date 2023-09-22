using Application.Dto;
using DataAccess.Models;

namespace Application.Services;

public interface IMessageService
{
    Task<EmailMessageDto> CreateEmailMessageAsync(Guid messageSource, string title, string text, string sender, CancellationToken cancellationToken);

    Task<PhoneMessageDto> CreatePhoneMessageAsync(Guid messageSource, string text, string sender, CancellationToken cancellationToken);
}
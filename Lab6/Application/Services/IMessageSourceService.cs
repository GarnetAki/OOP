using Application.Dto;
using DataAccess.Models;

namespace Application.Services;

public interface IMessageSourceService
{
    Task<PhoneDto> CreatePhoneAsync(ICollection<Guid> accountIds, string phone, CancellationToken cancellationToken);

    Task<EmailDto> CreateEmailAsync(ICollection<Guid> accountIds, string email, CancellationToken cancellationToken);

    Task<PhoneDto> RemovePhoneAsync(Guid id, CancellationToken cancellationToken);

    Task<EmailDto> RemoveEmailAsync(Guid id, CancellationToken cancellationToken);

    Task<PhoneDto> GetPhoneAsync(Guid id, CancellationToken cancellationToken);

    Task<EmailDto> GetEmailAsync(Guid id, CancellationToken cancellationToken);

    Task<MessageSourceDto> AddAccountAsync(Guid id, Guid accountId, CancellationToken cancellationToken);

    Task<MessageSourceDto> RemoveAccountAsync(Guid id, Guid accountId, CancellationToken cancellationToken);
}
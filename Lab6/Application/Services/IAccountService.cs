using Application.Dto;
using DataAccess.Models;

namespace Application.Services;

public interface IAccountService
{
    Task<AccountDto> CreateAccountAsync(ICollection<Guid> userIds, ICollection<Guid> messageSourceIds, CancellationToken cancellationToken);

    Task<AccountDto> RemoveAccountAsync(Guid id, CancellationToken cancellationToken);

    Task<AccountDto> AddUserAsync(Guid id, Guid userId, CancellationToken cancellationToken);

    Task<AccountDto> AddMessageSourceAsync(Guid id, Guid messageSourceId, CancellationToken cancellationToken);

    Task<AccountDto> RemoveUserAsync(Guid id, Guid userId, CancellationToken cancellationToken);

    Task<AccountDto> RemoveMessageSourceAsync(Guid id, Guid messageSourceId, CancellationToken cancellationToken);

    Task<AccountDto> GetAccountAsync(Guid id, CancellationToken cancellationToken);
}
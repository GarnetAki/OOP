using Application.Dto;

namespace Application.Services;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(string login, string password, Guid chiefId, ICollection<Guid> accountIds, CancellationToken cancellationToken);

    Task<UserDto> RemoveUserAsync(Guid id, CancellationToken cancellationToken);

    Task<UserDto> ChangeUserAsync(Guid id, string login, string password, Guid chiefId, CancellationToken cancellationToken);

    Task<UserDto> AddSubordinateIdAsync(Guid id, Guid subordinateId, CancellationToken cancellationToken);

    Task<UserDto> AddAccountIdAsync(Guid id, Guid accountId, CancellationToken cancellationToken);

    Task<UserDto> RemoveAccountIdAsync(Guid id, Guid accountId, CancellationToken cancellationToken);

    Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);
}
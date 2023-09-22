using System.Globalization;
using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using MPSExceptions;

namespace Application.Services.Implementations;

internal class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserDto> CreateUserAsync(string login, string password, Guid chiefId, ICollection<Guid> accountIds, CancellationToken cancellationToken)
    {
        ValidateString(login);
        ValidateString(password);

        if (_context.Users.Any(u => u.Login.Equals(login)))
            throw ServiceExceptions.LoginException(login);

        var accounts = new List<Account>();
        var newGuid = Guid.NewGuid();
        foreach (Guid id in accountIds)
        {
            accounts.Add(await _context.Accounts.GetEntityAsync(id, cancellationToken));
        }

        if (!chiefId.Equals(Guid.Empty) && _context.Users.FirstOrDefault(u => u.Id.Equals(chiefId)) == null)
            throw ServiceExceptions.IdException(chiefId);

        var user = new User(newGuid, login, password, chiefId, accounts);
        _context.Users.Add(user);
        if (!chiefId.Equals(Guid.Empty))
            _context.Users.First(u => u.Id.Equals(chiefId)).Subordinates.Add(user);

        foreach (Account account in accounts)
        {
            account.Users.Add(user);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> RemoveUserAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        foreach (User subordinate in user.Subordinates)
        {
            subordinate.ChiefId = Guid.Empty;
        }

        foreach (Account account in user.Accounts)
        {
            account.Users.Remove(user);
        }

        if (!user.ChiefId.Equals(Guid.Empty))
            _context.Users.First(u => u.Id.Equals(user.ChiefId)).Subordinates.Remove(user);

        _context.Users.Remove(user);
        user.Accounts = new List<Account>();
        user.ChiefId = Guid.Empty;
        user.Subordinates = new List<User>();
        user.Reports = new List<Report>();
        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> ChangeUserAsync(Guid id, string login, string password, Guid chiefId, CancellationToken cancellationToken)
    {
        ValidateString(login);
        ValidateString(password);
        if (_context.Users.FirstOrDefault(u => u.Login.Equals(login)) != null && !_context.Users.First(u => u.Login.Equals(login)).Id.Equals(id))
            throw ServiceExceptions.LoginException(login);

        User user = _context.Users.First(u => u.Id.Equals(id));
        if (!chiefId.Equals(user.ChiefId))
        {
            switch (chiefId.Equals(Guid.Empty))
            {
                case false when _context.Users.FirstOrDefault(u => u.Id.Equals(chiefId)) != null:
                    _context.Users.First(u => u.Id.Equals(chiefId)).Subordinates.Add(user);
                    break;
                case false:
                    throw ServiceExceptions.IdException(chiefId);
            }

            if (!user.ChiefId.Equals(Guid.Empty))
                _context.Users.First(u => u.Id.Equals(user.ChiefId)).Subordinates.Remove(user);

            user.ChiefId = chiefId;
        }

        user.Login = login;
        user.Password = password;
        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> AddSubordinateIdAsync(Guid id, Guid subordinateId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        User subordinateUser = _context.Users.First(u => u.Id.Equals(subordinateId));
        if (user.Subordinates.Contains(subordinateUser))
            throw ServiceExceptions.SubordinateIdException(subordinateId);

        if (!_context.Users.First(u => u.Id.Equals(subordinateId)).ChiefId.Equals(Guid.Empty))
            _context.Users.First(us => us.Id.Equals(_context.Users.First(u => u.Id.Equals(subordinateId)).ChiefId)).Subordinates.Remove(subordinateUser);

        _context.Users.First(u => u.Id.Equals(subordinateId)).ChiefId = id;
        user.Subordinates.Add(subordinateUser);
        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> AddAccountIdAsync(Guid id, Guid accountId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        Account account = _context.Accounts.First(a => a.Id.Equals(accountId));
        if (user.Accounts.Contains(account))
            throw ServiceExceptions.AccountInUserException(id, accountId);

        if (account.Users.Contains(user))
            throw ServiceExceptions.UserInAccountException(id, accountId);

        _context.Accounts.First(a => a.Id.Equals(accountId)).Users.Add(user);
        user.Accounts.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> RemoveAccountIdAsync(Guid id, Guid accountId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        Account account = _context.Accounts.First(a => a.Id.Equals(accountId));

        user.Accounts.Remove(account);
        account.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.AsDto();
    }

    public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        await _context.SaveChangesAsync(cancellationToken);
        return user.AsDto();
    }

    private static void ValidateString(string str)
    {
        if (str == null)
            throw new ArgumentNullException(str);

        if (str.Any(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.LowercaseLetter &&
                         CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.UppercaseLetter &&
                         CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber))
            throw ServiceExceptions.StringFormatException(str);

        if (str.Equals(string.Empty))
            throw ServiceExceptions.StringFormatException(str);
    }
}
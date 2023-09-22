using System.Globalization;
using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _context;

    public AccountService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> CreateAccountAsync(ICollection<Guid> userIds, ICollection<Guid> messageSourceIds, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        var users = userIds.Select(us => _context.Users.First(u => u.Id.Equals(us))).ToList();
        var sources = messageSourceIds.Select(ms => _context.MessageSources.First(u => u.Id.Equals(ms))).ToList();
        var account = new Account(newGuid, users, sources);

        foreach (Guid messageSourceId in messageSourceIds)
        {
            MessageSource source = _context.MessageSources.First(ms => ms.Id.Equals(messageSourceId));
            if (source.Accounts.FirstOrDefault(a => a.Id.Equals(account.Id)) == null)
                source.Accounts.Add(account);
        }

        foreach (Guid userId in userIds)
        {
            User user = _context.Users.First(ms => ms.Id.Equals(userId));
            if (user.Accounts.FirstOrDefault(a => a.Id.Equals(account.Id)) == null)
                user.Accounts.Add(account);
        }

        _context.Add(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> RemoveAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = _context.Accounts.First(a => a.Id.Equals(id));

        foreach (MessageSource messageSource in account.MessageSources)
        {
            messageSource.Accounts.Remove(account);
        }

        foreach (User user in account.Users)
        {
            user.Accounts.Remove(account);
        }

        _context.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> AddUserAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        Account account = _context.Accounts.First(a => a.Id.Equals(id));
        User user = _context.Users.First(u => u.Id.Equals(userId));

        if (!user.Accounts.Contains(account))
            user.Accounts.Add(account);

        if (!account.Users.Contains(user))
            account.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> AddMessageSourceAsync(Guid id, Guid messageSourceId, CancellationToken cancellationToken)
    {
        Account account = _context.Accounts.First(a => a.Id.Equals(id));
        MessageSource messageSource = _context.MessageSources.First(u => u.Id.Equals(messageSourceId));

        if (!messageSource.Accounts.Contains(account))
            messageSource.Accounts.Add(account);

        if (!account.MessageSources.Contains(messageSource))
            account.MessageSources.Add(messageSource);

        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> RemoveUserAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        Account account = _context.Accounts.First(a => a.Id.Equals(id));
        User user = _context.Users.First(u => u.Id.Equals(userId));

        if (user.Accounts.Contains(account))
            user.Accounts.Remove(account);

        if (account.Users.Contains(user))
            account.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> RemoveMessageSourceAsync(Guid id, Guid messageSourceId, CancellationToken cancellationToken)
    {
        Account account = _context.Accounts.First(a => a.Id.Equals(id));
        MessageSource messageSource = _context.MessageSources.First(u => u.Id.Equals(messageSourceId));

        if (messageSource.Accounts.Contains(account))
            messageSource.Accounts.Remove(account);

        if (account.MessageSources.Contains(messageSource))
            account.MessageSources.Remove(messageSource);

        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }

    public async Task<AccountDto> GetAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        Account account = _context.Accounts.First(a => a.Id.Equals(id));
        await _context.SaveChangesAsync(cancellationToken);

        return account.AsDto();
    }
}
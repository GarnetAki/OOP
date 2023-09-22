using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using MPSExceptions;

namespace Application.Services.Implementations;

public class MessageSourceService : IMessageSourceService
{
    private readonly DatabaseContext _context;

    public MessageSourceService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PhoneDto> CreatePhoneAsync(ICollection<Guid> accountIds, string phone, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        var accounts = new List<Account>();
        foreach (Guid id in accountIds)
        {
            accounts.Add(await _context.Accounts.GetEntityAsync(id, cancellationToken));
        }

        var source = new Phone(newGuid, accounts, phone);

        foreach (Account account in accounts)
        {
            account.MessageSources.Add(source);
        }

        _context.Phones.Add(source);

        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<EmailDto> CreateEmailAsync(ICollection<Guid> accountIds, string email, CancellationToken cancellationToken)
    {
        var newGuid = Guid.NewGuid();
        var accounts = new List<Account>();
        foreach (Guid id in accountIds)
        {
            accounts.Add(await _context.Accounts.GetEntityAsync(id, cancellationToken));
        }

        var source = new Email(newGuid, accounts, email);

        foreach (Account account in accounts)
        {
            account.MessageSources.Add(source);
        }

        _context.Emails.Add(source);

        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<PhoneDto> RemovePhoneAsync(Guid id, CancellationToken cancellationToken)
    {
        Phone source = _context.Phones.First(p => p.Id.Equals(id));
        foreach (Account account in source.Accounts)
        {
            account.MessageSources.Remove(source);
        }

        foreach (Message message in source.Messages)
        {
            _context.Messages.Remove(message);
        }

        _context.MessageSources.Remove(source);

        source.Accounts = new List<Account>();
        source.Messages = new List<Message>();

        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<EmailDto> RemoveEmailAsync(Guid id, CancellationToken cancellationToken)
    {
        Email source = _context.Emails.First(p => p.Id.Equals(id));
        foreach (Account account in source.Accounts)
        {
            account.MessageSources.Remove(source);
        }

        foreach (Message message in source.Messages)
        {
            _context.Messages.Remove(message);
        }

        _context.MessageSources.Remove(source);

        source.Accounts = new List<Account>();
        source.Messages = new List<Message>();

        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<PhoneDto> GetPhoneAsync(Guid id, CancellationToken cancellationToken)
    {
        Phone source = _context.Phones.First(p => p.Id.Equals(id));
        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<EmailDto> GetEmailAsync(Guid id, CancellationToken cancellationToken)
    {
        Email source = _context.Emails.First(p => p.Id.Equals(id));
        await _context.SaveChangesAsync(cancellationToken);

        return source.AsDto();
    }

    public async Task<MessageSourceDto> AddAccountAsync(Guid id, Guid accountId, CancellationToken cancellationToken)
    {
        MessageSource messageSource = _context.MessageSources.First(s => s.Id.Equals(id));
        Account account = _context.Accounts.First(s => s.Id.Equals(accountId));
        if (!messageSource.Accounts.Contains(account))
            messageSource.Accounts.Add(account);

        if (!account.MessageSources.Contains(messageSource))
            account.MessageSources.Add(messageSource);

        await _context.SaveChangesAsync(cancellationToken);

        return messageSource.AsDto();
    }

    public async Task<MessageSourceDto> RemoveAccountAsync(Guid id, Guid accountId, CancellationToken cancellationToken)
    {
        MessageSource messageSource = _context.MessageSources.First(s => s.Id.Equals(id));
        Account account = _context.Accounts.First(s => s.Id.Equals(accountId));
        messageSource.Accounts.Remove(account);
        account.MessageSources.Remove(messageSource);

        await _context.SaveChangesAsync(cancellationToken);

        return messageSource.AsDto();
    }
}
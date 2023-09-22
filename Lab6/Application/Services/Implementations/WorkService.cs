using Application.Dto;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using MPSExceptions;

namespace Application.Services.Implementations;

public class WorkService : IWorkService
{
    private readonly DatabaseContext _context;

    public WorkService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Guid> LogInAsync(string login, string password, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Login.Equals(login));
        if (!user.Password.Equals(password))
            throw ServiceExceptions.AuthorizedException();

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<List<AccountDto>> GetListOfAccountsAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        var accounts = user.Accounts.Select(a => a.AsDto()).ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return accounts;
    }

    public async Task<List<MessageSourceDto>> GetListOfSourcesAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        IEnumerable<MessageSource> sources = user.Accounts.SelectMany(a => a.MessageSources);
        var sourcesDto = sources.Select(s => s.AsDto()).ToList();

        await _context.SaveChangesAsync(cancellationToken);

        return sourcesDto;
    }

    public async Task<List<MessageDto>> GetMessagesAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        ICollection<MessageSource> sources = user.Accounts.SelectMany(a => a.MessageSources).ToList();
        var messages = new List<MessageDto>();
        foreach (MessageSource source in sources)
        {
            messages.AddRange(source.Messages.Select(m => m.AsDto()).ToList());
        }

        await _context.SaveChangesAsync(cancellationToken);

        return messages;
    }

    public async Task<string> GetMessageTextAsync(Guid id, Guid messageId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        ICollection<MessageSource> sources = user.Accounts.SelectMany(a => a.MessageSources).ToList();
        var messages = new List<Message>();
        foreach (MessageSource source in sources)
        {
            messages.AddRange(source.Messages);
        }

        string text = " ";
        Message message = messages.First(m => m.Id.Equals(messageId));
        if (message is EmailMessage)
            text = TakeEmailTextAsync(messageId);

        if (message is PhoneMessage)
            text = TakePhoneTextAsync(messageId);

        await _context.SaveChangesAsync(cancellationToken);

        return text;
    }

    public async Task<MessageDto> ChangeStatusToProcessedAsync(Guid id, Guid messageId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        ICollection<MessageSource> sources = user.Accounts.SelectMany(a => a.MessageSources).ToList();
        Message messageReturn = new Message();
        foreach (MessageSource source in sources)
        {
            Message? message = source.Messages.FirstOrDefault(m => m.Id.Equals(messageId));
            if (message != null)
            {
                message.Status = MessageStatus.Processed;
                messageReturn = message;
                source.Count++;
                user.Count++;
                break;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return messageReturn.AsDto();
    }

    public async Task<MessageDto> ChangeStatusToReceivedAsync(Guid id, Guid messageId, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        ICollection<MessageSource> sources = user.Accounts.SelectMany(a => a.MessageSources).ToList();
        var messages = new List<Message>();
        foreach (MessageSource source in sources)
        {
            messages.AddRange(source.Messages);
        }

        Message message = messages.First(m => m.Id.Equals(messageId));
        message.Status = MessageStatus.Received;

        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }

    public async Task<ReportDto> MakeReportAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        var newGuid = Guid.NewGuid();
        var sourceParts = new List<ReportSourcePart>();
        var userParts = new List<ReportUserPart>();
        foreach (User subordinate in user.Subordinates)
        {
            var userPart = new ReportUserPart(Guid.NewGuid(), subordinate.Id, subordinate.Count);
            userParts.Add(userPart);
            _context.ReportUserParts.Add(userPart);
            subordinate.Count = 0;
            foreach (Account account in subordinate.Accounts)
            {
                foreach (MessageSource source in account.MessageSources)
                {
                    var sourcePart = new ReportSourcePart(Guid.NewGuid(), source.Id, source.Count);
                    sourceParts.Add(sourcePart);
                    _context.ReportSourceParts.Add(sourcePart);
                    source.Count = 0;
                }
            }
        }

        var report = new Report(newGuid, user, sourceParts, userParts);
        _context.Reports.Add(report);
        user.Reports.Add(report);

        await _context.SaveChangesAsync(cancellationToken);

        return report.AsDto();
    }

    public async Task<List<ReportDto>> GetReportsAsync(Guid id, CancellationToken cancellationToken)
    {
        User user = _context.Users.First(u => u.Id.Equals(id));
        var reports = user.Reports.Select(r => r.AsDto()).ToList();
        await _context.SaveChangesAsync(cancellationToken);

        return reports;
    }

    private string TakeEmailTextAsync(Guid id)
    {
        EmailMessage message = _context.EmailMessages.First(m => m.Id.Equals(id));
        string text = message.Title + '\n' + message.Text;
        return text;
    }

    private string TakePhoneTextAsync(Guid id)
    {
        PhoneMessage message = _context.PhoneMessages.First(m => m.Id.Equals(id));
        string text = message.Text;
        return text;
    }
}
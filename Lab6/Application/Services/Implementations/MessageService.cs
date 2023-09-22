using Application.Dto;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using MPSExceptions;

namespace Application.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly DatabaseContext _context;

    public MessageService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<EmailMessageDto> CreateEmailMessageAsync(Guid messageSource, string title, string text, string sender, CancellationToken cancellationToken)
    {
        Guid newGuid = Guid.NewGuid();
        Email email = _context.Emails.First(e => e.Id.Equals(messageSource));

        var message = new EmailMessage(newGuid, email, title, text, sender);
        email.Messages.Add(message);

        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }

    public async Task<PhoneMessageDto> CreatePhoneMessageAsync(Guid messageSource, string text, string sender, CancellationToken cancellationToken)
    {
        Guid newGuid = Guid.NewGuid();
        Phone phone = _context.Phones.First(e => e.Id.Equals(messageSource));

        var message = new PhoneMessage(newGuid, phone, text, sender);
        phone.Messages.Add(message);

        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }
}
using MPSExceptions;

namespace DataAccess.Models;

public class Message
{
    private MessageStatus _status = MessageStatus.New;

    public Message(Guid id, MessageSource messageSource)
    {
        Id = id;
        MessageSource = messageSource;
    }

#pragma warning disable CS8618
    public Message() { }
#pragma warning restore CS8618

    public Guid Id { get; set; }

    public MessageStatus Status
    {
        get => _status;
        set
        {
            if (value > _status) _status = value;
            else throw StatusException.InvalidStatusException();
        }
    }

    public virtual MessageSource MessageSource { get; set; }
}
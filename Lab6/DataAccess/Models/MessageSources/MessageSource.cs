namespace DataAccess.Models;

public abstract class MessageSource
{
    public MessageSource(Guid id, ICollection<Account> accounts)
    {
        Id = id;
        Accounts = accounts;
        Messages = new List<Message>();
        Count = 0;
    }

#pragma warning disable CS8618
    public MessageSource() { }
#pragma warning restore CS8618

    public int Count { get; set; }

    public Guid Id { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}
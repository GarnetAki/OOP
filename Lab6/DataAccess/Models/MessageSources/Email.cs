namespace DataAccess.Models;

public class Email : MessageSource
{
    public Email(Guid id, ICollection<Account> accounts, string emailAddress)
        : base(id, accounts)
    {
        EmailAddress = emailAddress;
        Messages = new List<Message>();
    }

#pragma warning disable CS8618
    public Email() { }
#pragma warning restore CS8618

    public string EmailAddress { get; protected init; }
}
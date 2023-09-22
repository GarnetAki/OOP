namespace DataAccess.Models;

public class Phone : MessageSource
{
    public Phone(Guid id, ICollection<Account> accounts, string phoneNumber)
        : base(id, accounts)
    {
        PhoneNumber = phoneNumber;
        Messages = new List<Message>();
    }

#pragma warning disable CS8618
    public Phone() { }
#pragma warning restore CS8618

    public string PhoneNumber { get; protected init; }
}
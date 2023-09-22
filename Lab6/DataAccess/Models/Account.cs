using Microsoft.VisualBasic;

namespace DataAccess.Models;

public class Account
{
    public Account(Guid id, ICollection<User> users, ICollection<MessageSource> messageSources)
    {
        Id = id;
        Users = users;
        MessageSources = messageSources;
    }

    public Account(Guid id)
        : this(id, new List<User>(), new List<MessageSource>())
    {
    }

#pragma warning disable CS8618
    public Account() { }
#pragma warning restore CS8618

    public Guid Id { get; set; }

    public virtual ICollection<User> Users { get; set; }

    public virtual ICollection<MessageSource> MessageSources { get; set; }
}
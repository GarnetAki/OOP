using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class User
{
    public User(Guid id, string login, string password, Guid chiefId, ICollection<User> subordinates, ICollection<Account> accounts)
    {
        Id = id;
        Login = login;
        Password = password;
        ChiefId = chiefId;
        Count = 0;
        Subordinates = subordinates;
        Accounts = accounts;
        Reports = new List<Report>();
    }

    public User(Guid id, string login, string password, Guid chiefId, ICollection<Account> accounts)
        : this(id, login, password, chiefId, new List<User>(), accounts)
    {
    }

#pragma warning disable CS8618
    public User() { }
#pragma warning restore CS8618

    public int Count { get; set; }

    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public Guid ChiefId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }

    public virtual ICollection<Report> Reports { get; set; }

    public virtual ICollection<User> Subordinates { get; set; }
}
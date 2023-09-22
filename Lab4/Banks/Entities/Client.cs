namespace Banks.Entities;

public interface IFirstnameBuilder
{
    ILastnameBuilder WithFirstname(string firstname);
}

public interface ILastnameBuilder
{
    IClientBuilder WithLastname(string lastname);
}

public interface IClientBuilder
{
    IClientBuilder WithAddress(Address address);

    IClientBuilder WithPassport(Passport passport);

    IClientBuilder WithEmail(Email email);

    Client Build();
}

public class Client : IClient
{
    private List<IAccount> _accounts;

    private Client(string firstname, string lastname, Address? address, Passport? passport, Email? email)
    {
        _accounts = new List<IAccount>();
        Firstname = firstname;
        Lastname = lastname;
        Address = address;
        Passport = passport;
        Email = email;
        Id = Guid.NewGuid();
        SetVerified();
        SetSubscribed();
    }

    public IReadOnlyList<IAccount> Accounts => _accounts;

    public bool IsVerified { get; private set; }

    public bool IsSubscribed { get; private set; }

    public Guid Id { get; }

    public string Firstname { get; private set; }

    public string Lastname { get; private set; }

    public Address? Address { get; private set; }

    public Email? Email { get; private set; }

    public Passport? Passport { get; private set; }

    public static ClientBuilder Builder() => new ClientBuilder();

    public Guid AddAccount(IAccount account)
    {
        _accounts.Add(account);
        return account.Id;
    }

    public void InitOrChangeAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentNullException();
        SetVerified();
    }

    public void InitOrChangeEmail(Email email)
    {
        Email = email ?? throw new ArgumentNullException();
        SetSubscribed();
    }

    public void InitOrChangePassport(Passport newPassport)
    {
        Passport = newPassport ?? throw new ArgumentNullException();
        SetVerified();
    }

    public void ChangeFirstname(string newFirstname)
    {
        Firstname = newFirstname ?? throw new ArgumentNullException();
    }

    public void ChangeLastname(string newLastname)
    {
        Lastname = newLastname ?? throw new ArgumentNullException();
    }

    private void SetSubscribed()
    {
        IsSubscribed = Email != null;
    }

    private void SetVerified()
    {
        IsVerified = !(Address == null || Passport == null);
    }

    public class ClientBuilder : IFirstnameBuilder, ILastnameBuilder, IClientBuilder
    {
        private string _firstname = string.Empty;
        private string _lastname = string.Empty;
        private Address? _address;
        private Passport? _passport;
        private Email? _email;

        public ILastnameBuilder WithFirstname(string firstname)
        {
            _firstname = firstname ?? throw new ArgumentNullException();
            return this;
        }

        public IClientBuilder WithLastname(string lastname)
        {
            _lastname = lastname ?? throw new ArgumentNullException();
            return this;
        }

        public IClientBuilder WithAddress(Address address)
        {
            _address = address ?? throw new ArgumentNullException();
            return this;
        }

        public IClientBuilder WithPassport(Passport passport)
        {
            _passport = passport ?? throw new ArgumentNullException();
            return this;
        }

        public IClientBuilder WithEmail(Email email)
        {
            _email = email ?? throw new ArgumentNullException();
            return this;
        }

        public Client Build()
        {
            return new Client(_firstname, _lastname, _address, _passport, _email);
        }
    }
}
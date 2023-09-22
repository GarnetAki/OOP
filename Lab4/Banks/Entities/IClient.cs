namespace Banks.Entities;

public interface IClient
{
    IReadOnlyList<IAccount> Accounts { get; }

    bool IsVerified { get; }

    bool IsSubscribed { get; }

    Guid Id { get; }

    string Firstname { get; }

    string Lastname { get; }

    Email? Email { get; }

    Address? Address { get; }

    Passport? Passport { get; }

    Guid AddAccount(IAccount account);

    void InitOrChangeAddress(Address newAddress);

    void InitOrChangePassport(Passport newPassport);

    void InitOrChangeEmail(Email email);

    void ChangeFirstname(string newFirstname);

    void ChangeLastname(string newLastname);
}
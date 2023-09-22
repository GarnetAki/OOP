namespace Banks.Entities;

public class Email
{
    private List<string> _mails;

    public Email(string address)
    {
        _mails = new List<string>();
        Address = address;
    }

    public string Address { get; }

    public IReadOnlyList<string> Mails => _mails;

    public void SendMail(string message)
    {
        _mails.Add(message);
    }
}
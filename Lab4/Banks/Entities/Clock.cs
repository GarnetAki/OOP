namespace Banks.Entities;

public class Clock : IClock
{
    private readonly List<IAccount> _observers = new List<IAccount>();

    public Clock()
    {
        VirtualDate = DateTime.Today;
    }

    public DateTime VirtualDate { get; private set; }

    public void Attach(IAccount account)
    {
        _observers.Add(account);
    }

    public void Detach(IAccount account)
    {
        _observers.Remove(account);
    }

    public void Notify()
    {
        foreach (IAccount observer in _observers)
        {
            observer.Update(this);
        }
    }

    public void NextDay()
    {
        VirtualDate = VirtualDate.Add(TimeSpan.FromDays(1));
        Notify();
    }
}
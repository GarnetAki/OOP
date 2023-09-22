namespace Banks.Entities;

public interface IClock
{
    DateTime VirtualDate { get; }

    void Attach(IAccount account);

    void Detach(IAccount account);

    void Notify();

    void NextDay();
}
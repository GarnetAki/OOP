using Banks.Entities;

namespace Banks.Services;

public interface IAccountCreator
{
    IAccount Create(string type, IRate rate, IClock clock);
}
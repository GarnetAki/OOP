using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Address
{
    public Address(string address)
    {
        ValidateAddress(address);
        string[] substr = address.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        Street = substr[0];
        House = substr[1];
    }

    public string Street { get; }

    public string House { get; }

    public override string ToString()
    {
        return Street + " " + House;
    }

    private static void ValidateAddress(string fullname)
    {
        if (fullname == null)
            throw new ArgumentNullException();
        string[] subs = fullname.Split(' ');
        if (subs.Length != 2)
            throw AddressException.CreateInvalidFormat();
        if (!(subs[0][0] >= 'A' && subs[0][0] <= 'Z' && subs[0].Count(chr => chr is >= 'a' and <= 'z') == subs[0].Length - 1 &&
              subs[1].Count(chr => chr is >= '0' and <= '9') == subs[1].Length))
            throw AddressException.CreateInvalidChar();
    }
}
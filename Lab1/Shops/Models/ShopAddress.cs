using Shops.Exceptions;

namespace Shops.Models;

public class ShopAddress
{
    public ShopAddress(string address)
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
            throw ShopAddressException.CreateInvalidFormat();
        if (!(subs[0][0] >= 'A' && subs[0][0] <= 'Z' && subs[0].Count(chr => chr is >= 'a' and <= 'z') == subs[0].Length - 1 &&
        subs[1].Count(chr => chr is >= '0' and <= '9') == subs[1].Length))
            throw ShopAddressException.CreateInvalidChar();
    }
}
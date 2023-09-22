using Banks.Exceptions;

namespace Banks.Entities;

public class Address
{
    public Address(string address)
    {
        ValidateAddress(address);
        string[] substr = address.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
        City = substr[0];
        Street = substr[1];
        House = substr[2];
        Flat = substr[3];
    }

    public string City { get; }

    public string Street { get; }

    public string House { get; }

    public string Flat { get; }

    public override string ToString()
    {
        return City + " " + Street + " " + House + " " + Flat;
    }

    private static void ValidateAddress(string fullname)
    {
        if (fullname == null)
            throw new ArgumentNullException();

        string[] subs = fullname.Split(' ');
        if (subs.Length != 4)
            throw ClientException.CreateInvalidAddress();

        if (!(subs[0][0] >= 'A' && subs[0][0] <= 'Z' && subs[0].Count(chr => chr is >= 'a' and <= 'z') == subs[0].Length - 1 &&
              subs[1][0] >= 'A' && subs[1][0] <= 'Z' && subs[1].Count(chr => chr is >= 'a' and <= 'z') == subs[1].Length - 1 &&
              subs[2].Count(chr => chr is >= '0' and <= '9') == subs[2].Length &&
              subs[3].Count(chr => chr is >= '0' and <= '9') == subs[3].Length))
            throw ClientException.CreateInvalidAddress();
    }
}
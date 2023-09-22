using Banks.Exceptions;

namespace Banks.Entities;

public class Passport
{
    public Passport(string series, string number)
    {
        ValidatePassport(series, number);
        Series = series;
        Number = number;
    }

    public string Series { get; }

    public string Number { get; }

    private void ValidatePassport(string series, string number)
    {
        if (series.Length != 4 || number.Length != 6)
            throw ClientException.CreateInvalidPassport();

        if (series.Any(chr => chr is > '9' or < '0'))
            throw ClientException.CreateInvalidPassport();

        if (number.Any(chr => chr is > '9' or < '0'))
            throw ClientException.CreateInvalidPassport();
    }
}
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record PhoneNumber
{
    private PhoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }
    
    public string CountryCode { get; }
    public string Number { get; }

    public Result<PhoneNumber, string> Create(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return "Country code can not be null or empty";
        if (string.IsNullOrWhiteSpace(number))
            return "Number can not be null or empty";
        
        return new PhoneNumber(countryCode, number);
    }
}
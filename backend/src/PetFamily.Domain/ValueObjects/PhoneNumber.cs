using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record PhoneNumber
{
    //ef core
    private PhoneNumber() { }
    
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
        
        var countryCodePattern = @"^\+\d{1,3}$";
        var numberPattern = @"^\d{7,15}$";

        if (!Regex.IsMatch(countryCode, countryCodePattern))
            return "Invalid country code format. Expected format: +123";
        if (!Regex.IsMatch(number, numberPattern))
            return "Invalid number format. Expected 7 to 15 digits";
        
        return new PhoneNumber(countryCode, number);
    }
}
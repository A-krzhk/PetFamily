using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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

    public static Result<PhoneNumber, Error> Create(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            return Errors.General.ValueIsRequired("country code");
        if (string.IsNullOrWhiteSpace(number))
            return Errors.General.ValueIsRequired("phone-number");
        
        var countryCodePattern = @"^\+\d{1,3}$";
        var numberPattern = @"^\d{7,15}$";

        if (!Regex.IsMatch(countryCode, countryCodePattern))
            return Errors.General.ValueIsInvalid("country code");
        if (!Regex.IsMatch(number, numberPattern))
            return Errors.General.ValueIsInvalid("phone-number (Expected 7 to 15 digits)");
        
        return new PhoneNumber(countryCode, number);
    }
}
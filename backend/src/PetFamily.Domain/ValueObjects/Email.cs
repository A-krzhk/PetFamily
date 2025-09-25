using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Email
{
    //ef core
    private Email() { }
    
    private Email(string email)
    {
        EmailAddress = email;
    }
    public string EmailAddress { get; }
    
    public static Result<Email, Error> Create(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.ValueIsRequired(nameof(email));
        if (!Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
            return Errors.General.ValueIsInvalid(nameof(email));
        return new Email(email);
    }
}